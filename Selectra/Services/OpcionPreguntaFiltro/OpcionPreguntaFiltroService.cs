using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.OpcionPreguntaFiltro
{
    public class OpcionPreguntaFiltroService : IOpcionPreguntaFiltroService
    {
        private readonly SelectraContext _context;

        public OpcionPreguntaFiltroService(SelectraContext selectraContext)
        {
            _context = selectraContext;
        }

        public async Task<IEnumerable<ListaOpcionPreguntaFiltroDto>> GetListaOpcionPreguntaFiltroAsync() =>
            await _context.OpcionesPreguntasFiltros
                .Select(i => new ListaOpcionPreguntaFiltroDto
                {
                    opcionPreguntaId = i.opcionPreguntaId,
                    preguntaFiltroId = i.preguntaFiltroId,
                    textoOpcion = i.textoOpcion,
                    orden = i.orden 
                })
                .ToListAsync();

        public async Task<bool> GenerarOpcionPreguntaFiltroAsync(DetalleOpcionPreguntaFiltroDto opcionPreguntaFiltroDto)
        {
            var opcionPreguntaFiltro = new Models.OpcionPreguntaFiltro
            {
                preguntaFiltroId = opcionPreguntaFiltroDto.preguntaFiltroId,
                textoOpcion = opcionPreguntaFiltroDto.textoOpcion,
                orden = opcionPreguntaFiltroDto.orden,
                fechaCreacion = DateTime.Now,
                fechaUltMod = DateTime.Now,
                usuarioUltModId = opcionPreguntaFiltroDto.usuarioUltModId 
            };

            _context.OpcionesPreguntasFiltros.Add(opcionPreguntaFiltro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarOpcionPreguntaFiltroAsync(int idOpcionPreguntaFiltro, ActualizarOpcionPreguntaFiltroDto dto)
        {
            var opcion = await _context.OpcionesPreguntasFiltros.FindAsync(idOpcionPreguntaFiltro);
            if (opcion == null) return false;

            opcion.textoOpcion = dto.textoOpcion;

            if (dto.orden.HasValue) 
                opcion.orden = dto.orden;

            opcion.fechaUltMod = DateTime.Now;
           

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarOpcionPreguntaFiltroAsync(int idOpcionPreguntaFiltro)
        {
            var opcion = await _context.OpcionesPreguntasFiltros.FindAsync(idOpcionPreguntaFiltro);
            if (opcion == null) return false;

            _context.OpcionesPreguntasFiltros.Remove(opcion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}




