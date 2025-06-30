using Microsoft.EntityFrameworkCore;
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
    }
}
