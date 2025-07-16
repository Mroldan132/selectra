using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.TipoPreguntasFiltro
{
    public class TipoPreguntasFiltroService : ITipoPreguntasFiltroService
    {
        private readonly SelectraContext _context;

        public TipoPreguntasFiltroService(SelectraContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListaTipoPreguntasFiltroDto>> GetListaTipoPreguntasFiltroAsync()
        {
            return await _context.TipoPreguntasFiltros
                .Select(i => new ListaTipoPreguntasFiltroDto
                {
                    tipoPreguntaId = i.tipoPreguntaId,
                    nombre = i.nombre,
                })
                .ToListAsync();
        }

        public async Task<bool> CrearTipoPreguntasFiltroAsync(CrearTipoPreguntasFiltroDto dto)
        {
            var tipoPreguntas = new Models.TipoPreguntasFiltro
            {
                nombre = dto.nombre
            };

            _context.TipoPreguntasFiltros.Add(tipoPreguntas);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarTipoPreguntasFiltroAsync(ActualizarTipoPreguntasFiltroDto dto)
        {
            var tipoPreguntas = await _context.TipoPreguntasFiltros.FindAsync(dto.tipoPreguntaId);
            if (tipoPreguntas == null)
            {
                return false; // No se encontró
            }

            tipoPreguntas.nombre = dto.nombre;
            _context.TipoPreguntasFiltros.Update(tipoPreguntas);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
