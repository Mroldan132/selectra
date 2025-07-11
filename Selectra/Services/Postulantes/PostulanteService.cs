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

        public async Task<List<DetalleMisOfertasLaborales>> ListaMisOfertasLaborales(int usuarioId)
        {
            var aspirante = await _context.Aspirantes
                .Include(a => a.DatosPersonales)
                .FirstOrDefaultAsync(a => a.DatosPersonales.usuarioId == usuarioId);

            if (aspirante == null)
                return new List<DetalleMisOfertasLaborales>();

            var postulaciones = await _context.Postulantes
                .Where(p => p.aspiranteId == aspirante.aspiranteId)
                .ToListAsync();

            if (!postulaciones.Any())
                return new List<DetalleMisOfertasLaborales>();

            var ofertasIds = postulaciones.Select(p => p.ofertaId).ToList();

            var ofertas = await _context.OfertasLaborales
                .Include(o => o.EstadoOfertaLaboral)
                .Where(o => ofertasIds.Contains(o.ofertaId))
                .ToListAsync();

            var estadosPostulantes = await _context.EstadosPostulantes
                .ToDictionaryAsync(e => e.estadoPostulanteId, e => e);

            var resultado = postulaciones
                .Join(ofertas, p => p.ofertaId, o => o.ofertaId, (p, o) => new { p, o })
                .Select(x => new DetalleMisOfertasLaborales
                {
                    id = x.p.postulanteId,
                    fecha = x.p.fechaPostulacion.ToString("dd/MM/yyyy"),
                    estado = estadosPostulantes.ContainsKey(x.p.estadoPostulanteId)
                            ? estadosPostulantes[x.p.estadoPostulanteId].nombreEstado
                            : "Desconocido",
                    oferta = new MisOfertasLaboralesDto
                    {
                        idOfertta = x.o.ofertaId,
                        titulo = x.o.titulo,
                        estado = x.o.EstadoOfertaLaboral.nombreEstado
                    }
                })
                .ToList();

            return resultado;
        }
    }
}
