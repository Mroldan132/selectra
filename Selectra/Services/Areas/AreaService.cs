using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Areas
{
    public class AreaService : IAreaService
    {
        private readonly SelectraContext _context;
        public AreaService(SelectraContext selectraContext) { 
            _context = selectraContext;
        }

        public async Task<IEnumerable<ListaAreasDto>> GetListaAreasAsync() =>
            await _context.Areas
                .Select(a => new ListaAreasDto
                {
                    AreaId = a.areaId,
                    NombreArea = a.nombreArea,
                    Descripcion = a.descripcion,
                })
                .ToListAsync();
        public async Task<bool> GenerarAreaAsync(DetalleAreaDto areaDto)
        {
            var area = new Area
            {
                nombreArea = areaDto.nombreArea,
                descripcion = areaDto.descripcion,
                fechaCreacion = DateTime.Now,
                fechaUltMod = DateTime.Now
            };
            _context.Areas.Add(area);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActualizarAreaAsync(int idArea, ActualizarAreaDto actualizarAreaDto)
        {
            if (idArea <= 0)
            {
                throw new ArgumentException("El ID del area es necesario para insertar el requerimiento.", nameof(idArea));
            }

            var area = await _context.Areas.FindAsync(idArea);

            if (area == null)
            {
                throw new ApplicationException($"No se encontró el area con ID {idArea}.");
            }

            area.nombreArea = actualizarAreaDto.NombreArea;
            area.descripcion = actualizarAreaDto.Descripcion;
            area.fechaUltMod = DateTime.UtcNow;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Areas.Update(area);
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
