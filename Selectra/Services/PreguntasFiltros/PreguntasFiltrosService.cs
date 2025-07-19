using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.PreguntasFiltros
{
    public class PreguntasFiltrosService: IPreguntasFiltrosService
    {
        private readonly SelectraContext _context;
        public PreguntasFiltrosService(SelectraContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ListaPreguntasFiltrosDto>> GetListaPreguntasFiltrosAsync()
        {
            return await _context.PreguntasFiltros
                .Include(p => p.TipoPreguntasFiltros)
                .Select(i => new ListaPreguntasFiltrosDto
                {
                    preguntaFiltroId = i.preguntaFiltroId,
                    textoPregunta = i.textoPregunta,
                    tipoPreguntaId = i.tipoPreguntaId,
                    nombreTipoPregunta = i.TipoPreguntasFiltros.nombre, 
                    obligatoria = i.obligatoria,
                    fechaCreacion = i.fechaCreacion
                })
                .ToListAsync();
        }

        public async Task<bool> GenerarPreguntaFiltroAsync(DetallePreguntasFiltrosDto preguntaFiltroDto)
        {
            var preguntaFiltro = new Models.PreguntasFiltros
            {
                tipoPreguntaId = preguntaFiltroDto.tipoPreguntaId,
                textoPregunta = preguntaFiltroDto.textoPregunta,
                fechaCreacion = DateTime.Now,
                fechaUltMod = DateTime.Now
  
            };
            _context.PreguntasFiltros.Add(preguntaFiltro);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActualizarPreguntaFiltroAsync(int idPreguntaFiltro, ActualizarPreguntasFiltrosDto dto)
        {
            var preguntaFiltro = await _context.PreguntasFiltros.FindAsync(idPreguntaFiltro);

            if (preguntaFiltro == null)
                return false;

            preguntaFiltro.tipoPreguntaId = dto.tipoPreguntaId;
            preguntaFiltro.textoPregunta = dto.textoPregunta;
            preguntaFiltro.obligatoria = dto.obligatoria;
            preguntaFiltro.fechaUltMod = DateTime.Now;

            _context.PreguntasFiltros.Update(preguntaFiltro);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarPreguntaFiltroAsync(int idPreguntaFiltro)
        {
            var preguntaFiltro = await _context.PreguntasFiltros.FindAsync(idPreguntaFiltro);

            if (preguntaFiltro == null)
                return false;

            _context.PreguntasFiltros.Remove(preguntaFiltro);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
