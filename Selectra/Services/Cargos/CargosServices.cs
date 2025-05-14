using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Cargos
{
    public class CargosServices : ICargosService
    {
        private readonly SelectraContext _context;

        public CargosServices(SelectraContext context) { 
            _context = context;
        }


        public async Task<IEnumerable<ListaCargosDto>> GetListaCargosAsync() =>
            await _context.Cargos
                .Select(c => new ListaCargosDto { 
                    CargoId = c.cargoId,
                    NombreCargo = c.nombreCargo,
                    Descripcion = c.descripcion,
                })
                .ToListAsync();
    }
}
