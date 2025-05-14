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
    }
}
