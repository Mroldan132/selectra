using Microsoft.AspNetCore.Http.HttpResults;
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

            if (estadoAprobado == null)
            {
                throw new Exception("Estado de requerimiento aprobado no encontrado.");
            }

            return await _context.RequerimientosPersonales
                .Where(r => r.estadoRequerimientoId == estadoAprobado.estadoRequerimientoId
                    && !_context.OfertasLaborales.Any(o => o.requerimientoId == r.requerimientoId))
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

        public async Task<DetalleOfertaLaboralDto> CrearOfertaLaboralRequerimiento(int requerimientoId)
        {
            var estadoAprobadoRequerimiento = await _context.EstadosRequerimientos
                .FirstOrDefaultAsync(e => e.codigoEstado == "APR");
            var estadoAprobadoAprobaciones = await _context.EstadosHistorialAprobaciones
                .FirstOrDefaultAsync(e => e.codigoEstado == "APR");

            if (estadoAprobadoRequerimiento == null || estadoAprobadoAprobaciones == null)
                throw new Exception("No se encontraron los estados de aprobación necesarios.");

            var requerimiento = await _context.RequerimientosPersonales
                .Include(r => r.HistorialAprobaciones)
                .FirstOrDefaultAsync(r =>
                    r.requerimientoId == requerimientoId &&
                    r.estadoRequerimientoId == estadoAprobadoRequerimiento.estadoRequerimientoId &&
                    r.HistorialAprobaciones.Any(ha => ha.estadoHistorialAprobacionId == estadoAprobadoAprobaciones.estadoHistorialAprobacionId)
                );

            if (requerimiento == null)
                throw new Exception("Requerimiento no encontrado o no aprobado.");

            return new DetalleOfertaLaboralDto()
            {
                requerimientoId = requerimiento.requerimientoId,
                titulo = requerimiento.tituloRequerimiento,
                descripcion = "",
                funciones = "",
                beneficios = "",
                competencias = "",
                sueldoOfrecido = requerimiento.sueldoPropuesto,
                areaId = requerimiento.areaId,
                cargoId = requerimiento.cargoId,
                responsable = requerimiento.solicitanteId,
                direccionTrabajo = "",
                referenciaUbicacion = "",
                fechaCreacion = DateTime.Now,
                fechaPublicacion = DateTime.Now,
                fechaCierre = null,
                fechaEstimadaIngreso = requerimiento.fechaDeseadaIngreso
            };
        }

        public async Task<bool> GenerarOfertaLaborarAsync(DetalleOfertaLaboralDto ofertaLaboralDto,int usuarioQueRegistraId)
        {
            var estadoOfertaLaboral = await _context.EstadosOfertaLaborales
                .FirstOrDefaultAsync(o => o.codigoEstado.Equals("PEN"));

            if(estadoOfertaLaboral == null) {
                throw new Exception("Estados de la oferta laboral no configurados.");
            }

            var ofertaLaboral = new OfertaLaboral
            {
                requerimientoId = ofertaLaboralDto.requerimientoId,
                titulo = ofertaLaboralDto.titulo,
                descripcion = ofertaLaboralDto.descripcion,
                funciones = ofertaLaboralDto.funciones,
                beneficios = ofertaLaboralDto.beneficios,
                competencias = ofertaLaboralDto.competencias,
                sueldoOfrecido = ofertaLaboralDto.sueldoOfrecido,
                areaId = ofertaLaboralDto.areaId,
                cargoId = ofertaLaboralDto.cargoId,
                responsableId = ofertaLaboralDto.responsable,
                direccionTrabajo = ofertaLaboralDto.direccionTrabajo,
                referenciaUbicacion = ofertaLaboralDto.referenciaUbicacion,
                estadoOfertaLaboralId = estadoOfertaLaboral.estadoOfertaLaboralId,
                fechaCreacion = DateTime.Now,
                fechaEstimadaIngreso = ofertaLaboralDto.fechaEstimadaIngreso,
                fechaUltMod = DateTime.Now,
                usuarioUltModId = usuarioQueRegistraId
            };
            _context.OfertasLaborales.Add(ofertaLaboral);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ListaOfertasLaboralesDto>> GetListOfertasLaboralesAsync() => 
           await _context.OfertasLaborales
                .Include(r => r.Area)
                .Include(r => r.EstadoOfertaLaboral)
                .Select(r => new ListaOfertasLaboralesDto
                {
                    ofertaLaboralId = r.ofertaId,
                    titulo = r.titulo,
                    area = r.Area.nombreArea,
                    sueldo = r.sueldoOfrecido,
                    estadoOferta = r.EstadoOfertaLaboral.nombreEstado,
                    fechaCreacion = r.fechaCreacion
                })
                .ToListAsync();

        public async Task<DetalleOfertaLaboralDto> DetalleOfertaLaboralRequerimientoAsync(int ofertaLaboralId) {
            var ofertaLaboral = await _context.OfertasLaborales
                .FirstOrDefaultAsync(i => i.ofertaId == ofertaLaboralId);

            if (ofertaLaboral == null)
                throw new Exception("Oferta laboral no encontrada.");

            return new DetalleOfertaLaboralDto()
            {
                requerimientoId = ofertaLaboral.requerimientoId ?? 0,
                ofertaId = ofertaLaboral.ofertaId,
                titulo = ofertaLaboral.titulo,
                descripcion = ofertaLaboral.descripcion,
                funciones = ofertaLaboral.funciones,
                beneficios = ofertaLaboral.beneficios,
                competencias = ofertaLaboral.competencias,
                sueldoOfrecido = ofertaLaboral.sueldoOfrecido,
                areaId = ofertaLaboral.areaId,
                cargoId = ofertaLaboral.cargoId,
                responsable = ofertaLaboral.responsableId,
                direccionTrabajo = ofertaLaboral.direccionTrabajo,
                referenciaUbicacion = ofertaLaboral.referenciaUbicacion,
                fechaCreacion = ofertaLaboral.fechaCreacion,
                fechaPublicacion = ofertaLaboral.fechaPublicacion,
                fechaCierre = ofertaLaboral.fechaCierre,
                fechaEstimadaIngreso = ofertaLaboral.fechaEstimadaIngreso
            };
        }
    }
}
