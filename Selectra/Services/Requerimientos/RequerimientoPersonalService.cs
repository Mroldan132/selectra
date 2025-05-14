using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Requerimiento
{
    public class RequerimientoPersonalService : IRequerimientoPersonalService
    {
        private readonly SelectraContext _context;

        public RequerimientoPersonalService(SelectraContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MisRequerimientosListDto>> GetMisRequerimientosAsync(int solicitantePersonalId)
        {
            var requerimientos = await _context.RequerimientosPersonales
                .Where(r => r.solicitanteId == solicitantePersonalId)
                .Include(r => r.TipoRequerimiento) 
                .Include(r => r.CargoSolicitado)    
                .Include(r => r.EstadoRequerimiento)
                .OrderByDescending(r => r.fechaCreacion) 
                .Select(r => new MisRequerimientosListDto
                {
                    RequerimientoId = r.requerimientoId,
                    TituloRequerimiento = r.tituloRequerimiento,
                    TipoRequerimientoNombre = r.TipoRequerimiento != null ? r.TipoRequerimiento.nombre : "N/A",
                    CargoNombre = r.CargoSolicitado != null ? r.CargoSolicitado.nombreCargo : "N/A",
                    FechaCreacion = r.fechaCreacion,
                    EstadoNombre = r.EstadoRequerimiento != null ? r.EstadoRequerimiento.nombreEstado : "N/A"
                })
                .ToListAsync();

            return requerimientos;
        }
        public async Task<RequerimientoCreadoDto> CrearRequerimientoAsync(CrearRequerimientoDto dto, int solicitanteId, int usuarioCreaId)
        {
            var estadoInicial = await _context.EstadosRequerimientos
                .FirstOrDefaultAsync(e => e.esEstadoInicial == true);

            if (estadoInicial == null)
            {
                throw new ApplicationException("No se encontró un estado inicial configurado para los requerimientos.");
            }

            var ahora = DateTime.UtcNow;

            var nuevoRequerimiento = new RequerimientoPersonal
            {
                tipoRequerimientoId = dto.TipoRequerimientoId,
                tituloRequerimiento = dto.TituloRequerimiento,
                solicitanteId = solicitanteId,
                areaId = dto.AreaId,
                cargoId = dto.CargoId,
                motivo = dto.Motivo,
                sueldoPropuesto = dto.SueldoPropuesto,
                fechaDeseadaIngreso = dto.FechaDeseadaIngreso,
                jefeDestinoId = dto.JefeDestinoId,
                estadoRequerimientoId = estadoInicial.estadoRequerimientoId,
                fechaCreacion = ahora,
                fechaUltMod = ahora,
                usuarioUltModId = usuarioCreaId
            };

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.RequerimientosPersonales.Add(nuevoRequerimiento);
                await _context.SaveChangesAsync();

                var primerPasoAprobacion = await _context.OrdenesAprobaciones
                    .Where(oa => oa.tipoRequerimientoId == nuevoRequerimiento.tipoRequerimientoId && oa.orden == 1)
                    .Include(oa => oa.RolAprobador)
                    .FirstOrDefaultAsync();

                if (primerPasoAprobacion != null)
                {
                    int? aprobadorDelPasoId = null;

                    var solicitante = await _context.Personales.FindAsync(solicitanteId);

                    if (primerPasoAprobacion.RolAprobador?.nombreRol == "Jefe Directo" || primerPasoAprobacion.RolAprobador?.nombreRol == "JefeAprobador")
                    {
                        if(solicitante != null && solicitante.jefeDirectoId.HasValue) 
                        {
                            aprobadorDelPasoId = solicitante.jefeDirectoId.Value;
                        }
                        else
                        {
                            throw new ApplicationException("El solicitante no tiene un jefe directo asignado.");
                        }
                    }

                    if (aprobadorDelPasoId.HasValue) 
                    { 
                        var estadoPendienteHistorial = await _context.EstadosHistorialAprobaciones
                                                        .FirstOrDefaultAsync(e => e.codigoEstado == "PEN");

                        if (estadoPendienteHistorial == null) 
                            throw new ApplicationException("Estado 'Pendiente' para historial de aprobación no configurado.");

                        var historialAprobacion = new HistorialAprobacion
                        {
                            requerimientoId = nuevoRequerimiento.requerimientoId,
                            ordenAprobacionId = primerPasoAprobacion.ordenAprobacionId,
                            aprobadorId = aprobadorDelPasoId.Value,
                            estadoHistorialAprobacionId = estadoPendienteHistorial.estadoHistorialAprobacionId,
                            fechaDecision = null,
                            observaciones = "Asignado para la aprobación",
                            fechaCreacion = ahora,
                            fechaUltMod = ahora,
                            usuarioUltModId = usuarioCreaId
                        };

                        _context.HistorialAprobaciones.Add(historialAprobacion);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        nuevoRequerimiento.estadoRequerimientoId = (await _context.EstadosRequerimientos.FirstAsync(e => e.codigoEstado == "PEN")).estadoRequerimientoId;
                        await _context.SaveChangesAsync();
                        throw  new($"ADVERTENCIA: No se asignó aprobador al primer paso para el requerimiento {nuevoRequerimiento.requerimientoId}. Se mantiene en estado '{estadoInicial.nombreEstado}'.");

                    }
                }
                else
                {
                    throw new($"ADVERTENCIA: No se encontró un flujo de aprobación para el tipo de requerimiento {nuevoRequerimiento.tipoRequerimientoId}. El requerimiento {nuevoRequerimiento.requerimientoId} se queda en estado '{estadoInicial.nombreEstado}'.");
                }

                await transaction.CommitAsync();

                return new RequerimientoCreadoDto
                {
                    RequerimientoId = nuevoRequerimiento.requerimientoId,
                    TituloRequerimiento = nuevoRequerimiento.tituloRequerimiento,
                    EstadoNombre = estadoInicial.nombreEstado, 
                    FechaCreacion = nuevoRequerimiento.fechaCreacion
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<RequerimientoDetalleDto> GetRequerimientoDetalleAsync(int requerimientoId, int usuarioActualPersonalId)
        {
            var requerimiento = await _context.RequerimientosPersonales
                .Include(r => r.TipoRequerimiento)
                .Include(r => r.AreaDestino) 
                .Include(r => r.CargoSolicitado) 
                .Include(r => r.Solicitante).ThenInclude(s => s.DatosPersonales) 
                .Include(r => r.JefeDestino).ThenInclude(jd => jd.DatosPersonales) 
                .Include(r => r.EstadoRequerimiento)
                .Include(r => r.UsuarioUltMod)
                .Include(r => r.HistorialAprobaciones)
                    .ThenInclude(ha => ha.Aprobador).ThenInclude(ap => ap.DatosPersonales) 
                .Include(r => r.HistorialAprobaciones)
                    .ThenInclude(ha => ha.EstadoHistorialAprobacion)
                .Include(r => r.HistorialAprobaciones)
                    .ThenInclude(ha => ha.OrdenAprobacion)
                .FirstOrDefaultAsync(r => r.requerimientoId == requerimientoId);

            if (requerimiento == null)
            {
                return null;
            }

            var dto = new RequerimientoDetalleDto
            {
                RequerimientoId = requerimiento.requerimientoId,
                TituloRequerimiento = requerimiento.tituloRequerimiento,
                TipoRequerimientoNombre = requerimiento.TipoRequerimiento?.nombre ?? string.Empty,
                AreaNombre = requerimiento.AreaDestino?.nombreArea ?? string.Empty,
                CargoNombre = requerimiento.CargoSolicitado?.nombreCargo ?? "N/A",
                SolicitanteNombre = $"{requerimiento.Solicitante?.DatosPersonales?.nombres} {requerimiento.Solicitante?.DatosPersonales?.apellidoPaterno}".Trim(),
                Motivo = requerimiento.motivo,
                SueldoPropuesto = requerimiento.sueldoPropuesto,
                FechaDeseadaIngreso = requerimiento.fechaDeseadaIngreso,
                JefeDestinoNombre = requerimiento.jefeDestinoId.HasValue && requerimiento.JefeDestino?.DatosPersonales != null ?
                    $"{requerimiento.JefeDestino.DatosPersonales.nombres} {requerimiento.JefeDestino.DatosPersonales.apellidoPaterno}".Trim() : string.Empty,
                EstadoActualNombre = requerimiento.EstadoRequerimiento?.nombreEstado ?? string.Empty,
                FechaCreacionRequerimiento = requerimiento.fechaCreacion,
                FechaFinProceso = requerimiento.fechaFinProceso,
                UsuarioUltModNombre = requerimiento.UsuarioUltMod?.codUsuario ?? string.Empty,
                FechaUltModRequerimiento = requerimiento.fechaUltMod,
                HistorialDeAprobaciones = requerimiento.HistorialAprobaciones
                    .OrderBy(ha => ha.OrdenAprobacion?.orden) // Ordenar por el paso de aprobación
                    .ThenBy(ha => ha.fechaCreacion)
                    .Select(ha => new HistorialAprobacionDetalleDto
                    {
                        HistorialAprobacionId = ha.historialAprobacionId,
                        NombrePaso = ha.OrdenAprobacion?.descripcionPaso ?? $"Paso {ha.OrdenAprobacion?.orden}",
                        Orden = ha.OrdenAprobacion?.orden ?? 0,
                        NombreAprobador = $"{ha.Aprobador?.DatosPersonales?.nombres} {ha.Aprobador?.DatosPersonales?.apellidoPaterno}".Trim(),
                        EstadoDecision = ha.EstadoHistorialAprobacion?.nombreEstado ?? string.Empty,
                        Observaciones = ha.observaciones,
                        FechaDecision = ha.fechaDecision,
                        FechaCreacionPaso = ha.fechaCreacion
                    }).ToList()
            };

            return dto;
        }
        public async Task<IEnumerable<ListaTipoRequerimientosDto>> GetListaTiposRequerimientosAsync() =>
            await _context.TiposRequerimientos
            .Select(t => new ListaTipoRequerimientosDto
            {
                TipoRequerimientoId = t.tipoRequerimientoId,
                Nombre = t.nombre
            })
            .ToListAsync();

    }
}
