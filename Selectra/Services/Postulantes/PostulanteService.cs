using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Postulantes
{
    public class PostulanteService : IPostulanteService
    {
        private readonly SelectraContext _context;

        public PostulanteService(SelectraContext context)
        {
            _context = context;
        }

        public async Task<bool> PostularOfertaLaboral(int ofertaLaboralId, int usuarioId)
        {
            var usuarioPostulante = await _context.Usuarios
                .FirstOrDefaultAsync(i => i.usuarioId == usuarioId);

            if (usuarioPostulante == null)
                return false;

            var aspirante = await _context.Aspirantes
                .Include(r => r.DatosPersonales)
                .FirstOrDefaultAsync(r => r.DatosPersonales.usuarioId == usuarioPostulante.usuarioId);

            if (aspirante == null)
                return false;

            var oferta = await _context.OfertasLaborales.FindAsync(ofertaLaboralId);

            var estadoInicial = await _context.EstadosPostulantes.FirstOrDefaultAsync(e => e.codigoEstado == "POS");
            if (oferta == null)
            {
                return false;
            }
            var postulante = new Postulante
            {
                ofertaId = ofertaLaboralId,
                aspiranteId = aspirante.aspiranteId,
                fechaPostulacion = DateTime.UtcNow,
                fuenteReclutamiento = "",
                estadoPostulanteId = estadoInicial.estadoPostulanteId,
                fechaCreacion = DateTime.UtcNow,
                fechaUltMod = DateTime.UtcNow,
                usuarioUltModId = null 
            };
            _context.Postulantes.Add(postulante);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<List<MisOfertasLaboralesDto>> ListaMisOfertasLaborales(int usuarioId)
        {
            // Paso 1: Buscar el aspirante relacionado al usuario
            var aspirante = await _context.Aspirantes
                .Include(a => a.DatosPersonales)
                .FirstOrDefaultAsync(a => a.DatosPersonales.usuarioId == usuarioId);

            if (aspirante == null)
                return new List<MisOfertasLaboralesDto>();

            // Paso 2: Buscar los postulantes (inscripciones) de ese aspirante
            var postulaciones = await _context.Postulantes
                .Where(p => p.aspiranteId == aspirante.aspiranteId)
                .ToListAsync();

            if (!postulaciones.Any())
                return new List<MisOfertasLaboralesDto>();

            // Paso 3: Obtener las ofertas laborales y el estado de la postulación
            var ofertasIds = postulaciones.Select(p => p.ofertaId).ToList();

            var ofertas = await _context.OfertasLaborales
                .Where(o => ofertasIds.Contains(o.ofertaId))
                .ToListAsync();

            var estadosPostulantes = await _context.EstadosPostulantes
                .ToDictionaryAsync(e => e.estadoPostulanteId, e => e);

            // Paso 4: Construir el DTO de respuesta
            var resultado = postulaciones
                .Join(ofertas, p => p.ofertaId, o => o.ofertaId, (p, o) => new { p, o })
                .Select(x => new MisOfertasLaboralesDto
                {
                    titulo = x.o.descripcion,
                    estado = estadosPostulantes.ContainsKey(x.p.estadoPostulanteId)
                        ? estadosPostulantes[x.p.estadoPostulanteId].codigoEstado
                        : "Desconocido"
                })
                .ToList();

            return resultado;
        }
    }
}
