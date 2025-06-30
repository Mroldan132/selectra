using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Cargos
{
    public class CargosService : ICargosService
    {
        private readonly SelectraContext _context;

        public CargosService(SelectraContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListaCargosDto>> GetListaCargosAsync() =>
            await _context.Cargos
                .Select(c => new ListaCargosDto
                {
                    CargoId = c.cargoId,
                    NombreCargo = c.nombreCargo,
                    Descripcion = c.descripcion,
                })
                .ToListAsync();

        public async Task<bool> GenerarCargoAsync(DetalleCargoDto cargoDto)
        {
            var cargo = new Cargo
            {
                nombreCargo = cargoDto.nombreCargo,
                descripcion = cargoDto.descripcion,
                fechaCreacion = DateTime.Now,
                fechaUltMod = DateTime.Now
            };

            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarCargoAsync(int idCargo, ActualizarCargoDto actualizarCargoDto)
        {
            if (idCargo <= 0)
            {
                throw new ArgumentException("El ID del cargo es necesario para actualizar el registro.", nameof(idCargo));
            }

            var cargo = await _context.Cargos.FindAsync(idCargo);

            if (cargo == null)
            {
                throw new ApplicationException($"No se encontró el cargo con ID {idCargo}.");
            }

            cargo.nombreCargo = actualizarCargoDto.NombreCargo;
            cargo.descripcion = actualizarCargoDto.Descripcion;
            cargo.fechaUltMod = DateTime.UtcNow;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Cargos.Update(cargo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
