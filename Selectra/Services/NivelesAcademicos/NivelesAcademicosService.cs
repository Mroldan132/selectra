using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.NivelesAcademicos
{
    public class NivelesAcademicosService : INivelesAcademicosService
    {
        private readonly SelectraContext _context;

        public NivelesAcademicosService(SelectraContext context)
        {
            _context = context;
        }

        public async Task<List<ListaNivelesAcademicosDto>> GetListaNivelesAcademicosAsync() =>
            await _context.NivelAcademicos.Select(i => new ListaNivelesAcademicosDto
            {
                Id = i.nivelAcademicoId,
                Nombre = i.nombre
            }).ToListAsync();
    }
}
