using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.OfertasLaborales
{
    public class OfertasLaboralesServices : IOfertasLaboralesServices
    {
        private readonly SelectraContext _context;
        public OfertasLaboralesServices(SelectraContext context)
        {
            _context = context;
        }

        public async Task<List<RequerimientosAprobadosDto>> GetRequerimientosAprobadosAsync()
        {
            var estadoAprobado = await _context.EstadosRequerimientos
                .FirstOrDefaultAsync(e => e.codigoEstado == "APR");

            if(estadoAprobado == null)
            {
                throw new Exception("Estado de requerimiento aprobado no encontrado.");
            }

            return await _context.RequerimientosPersonales
            .Where(r => r.estadoRequerimientoId == estadoAprobado.estadoRequerimientoId)
            .Include(r => r.HistorialAprobaciones)
            .Select(r => new RequerimientosAprobadosDto
            {
                requerimientoId = r.requerimientoId,
                nombreRequerimiento = r.tituloRequerimiento,
                fechaSolicitud = r.fechaCreacion,
                fechaAprobacion = r.fechaFinProceso,
                solicitante = r.Solicitante.DatosPersonales.apellidoPaterno + " " + r.Solicitante.DatosPersonales.nombres,
                aprobador = r.HistorialAprobaciones
                    .Select(ha => ha.Aprobador.DatosPersonales.apellidoPaterno + " " + ha.Aprobador.DatosPersonales.nombres)
                    .FirstOrDefault()
            })
            .ToListAsync();
        }

    }
}
