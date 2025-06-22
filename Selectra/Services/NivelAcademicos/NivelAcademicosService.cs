using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.NivelAcademicos
{
    public class NivelAcademicosService : INivelAcademicosService
    {
        private readonly SelectraContext _context;
        public NivelAcademicosService(SelectraContext context)
        {
            _context = context;
        }
        public async Task<List<ListaNivelAcademicosDto>> GetListaNivelAcademicosAsync()
        {
            return await _context.NivelAcademicos
                .Select(i => new ListaNivelAcademicosDto
                {
                    nivelAcademicoId = i.nivelAcademicoId,
                    nombre = i.nombre,
                })
                .ToListAsync();
        }
        public async Task<bool> CrearNivelAcademicosAsync(ListaNivelAcademicosDto dto)
        {
           var nivelAcademico = new Models.NivelAcademicos
            {
                nombre = dto.nombre,
            };
            _context.NivelAcademicos.Add(nivelAcademico);
          await _context.SaveChangesAsync();
            return true;

        }
       public async Task<bool> ActualizarNivelAcademicosAsync(ListaNivelAcademicosDto dto)
        {
            var nivelAcademico = new Models.NivelAcademicos
            {
               nivelAcademicoId = dto.nivelAcademicoId,
                nombre = dto.nombre,
            };
            _context.NivelAcademicos.Update(nivelAcademico);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
