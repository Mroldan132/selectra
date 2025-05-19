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
            var querySolicitante = _context.RequerimientosPersonales
                .Where(r => r.solicitanteId == solicitantePersonalId);

            var queryAprobador = _context.RequerimientosPersonales
                .Where(r => r.HistorialAprobaciones.Any(ha => ha.aprobadorId == solicitantePersonalId));

            var requerimientos = await querySolicitante
                .Union(queryAprobador)
                .Include(r => r.TipoRequerimiento)
                .Include(r => r.CargoSolicitado)
                .Include(r => r.EstadoRequerimiento)
                .Include(r => r.HistorialAprobaciones)
                    .ThenInclude(ha => ha.Aprobador)
                .Include(r => r.HistorialAprobaciones)
                    .ThenInclude(ha => ha.EstadoHistorialAprobacion)
                .OrderByDescending(r => r.fechaCreacion)
                .Select(r => new MisRequerimientosListDto
                {
                    RequerimientoId = r.requerimientoId,
                    TituloRequerimiento = r.tituloRequerimiento,
                    TipoRequerimientoNombre = r.TipoRequerimiento != null ? r.TipoRequerimiento.nombre : "N/A",
                    CargoNombre = r.CargoSolicitado != null ? r.CargoSolicitado.nombreCargo : "N/A",
                    FechaCreacion = r.fechaCreacion,
                    EstadoNombre = r.EstadoRequerimiento != null ? r.EstadoRequerimiento.nombreEstado : "N/A",
                    HistorialDeAprobaciones = r.HistorialAprobaciones
                        .OrderBy(ha => ha.fechaCreacion)
                        .Select(ha => new HistorialAprobacionDto
                        {
                            historialAprobacionId = ha.historialAprobacionId,
                            codigoEstado = ha.EstadoHistorialAprobacion != null ? ha.EstadoHistorialAprobacion.codigoEstado : string.Empty,
                            observaciones = ha.observaciones
                        }).ToList()
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
                int? aprobadorDelPasoId = null;
                if (primerPasoAprobacion != null)
                {

                    var solicitante = await _context.Personales.FindAsync(solicitanteId);

                    if (primerPasoAprobacion.RolAprobador?.nombreRol == "JefeDirecto" || primerPasoAprobacion.RolAprobador?.nombreRol == "JefeAprobador")
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
                    FechaCreacion = nuevoRequerimiento.fechaCreacion,
                    AprobadorId = aprobadorDelPasoId
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<RequerimientoCreadoDto> ActualizarRequerimientoAsync(int idRequerimiento, ActualizarRequerimientoDto actualizarRequerimientoDto,int usuarioEditaId)
        {
            if(idRequerimiento <= 0)
            {
                throw new ArgumentException("El ID del requerimiento es necesario para insertar el requerimiento.", nameof(idRequerimiento));
            }

            var requerimiento = await _context.RequerimientosPersonales.FindAsync(idRequerimiento);

            if (requerimiento == null)
            {
                throw new ApplicationException($"No se encontró el requerimiento con ID {idRequerimiento}.");
            }

            requerimiento.tipoRequerimientoId = actualizarRequerimientoDto.TipoRequerimientoId;
            requerimiento.tituloRequerimiento = actualizarRequerimientoDto.TituloRequerimiento;
            requerimiento.areaId = actualizarRequerimientoDto.AreaId;
            requerimiento.cargoId = actualizarRequerimientoDto.CargoId;
            requerimiento.motivo = actualizarRequerimientoDto.Motivo;
            requerimiento.sueldoPropuesto = actualizarRequerimientoDto.SueldoPropuesto;
            requerimiento.fechaDeseadaIngreso = actualizarRequerimientoDto.FechaDeseadaIngreso;
            requerimiento.jefeDestinoId = actualizarRequerimientoDto.JefeDestinoId;
            requerimiento.fechaUltMod = DateTime.UtcNow;
            requerimiento.usuarioUltModId = usuarioEditaId;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.RequerimientosPersonales.Update(requerimiento);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new RequerimientoCreadoDto
                {
                    RequerimientoId = requerimiento.requerimientoId,
                    TituloRequerimiento = requerimiento.tituloRequerimiento,
                    EstadoNombre = requerimiento.EstadoRequerimiento?.nombreEstado ?? "N/A",
                    FechaCreacion = requerimiento.fechaCreacion
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
                tipoRequerimientoId = requerimiento.tipoRequerimientoId,
                areaId = requerimiento.areaId,
                cargoId = requerimiento.cargoId,
                jefeDestinoId = requerimiento.jefeDestinoId,
                HistorialDeAprobaciones = requerimiento.HistorialAprobaciones
                    .OrderBy(ha => ha.OrdenAprobacion?.orden) 
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
        public async Task<string> AprobarRechazarRequerimientoAsync(AprobarRechazarRequerimientoDto aprobarRechazardto, int usuarioUltMod)
        {
            if (aprobarRechazardto.idRequerimiento <= 0)
                throw new ArgumentException("El ID del requerimiento es necesario para aprobar el requerimiento.", nameof(aprobarRechazardto.idRequerimiento));

            var estadoHistorialAprobacion = await _context.EstadosHistorialAprobaciones
                .FirstOrDefaultAsync(e => e.codigoEstado == aprobarRechazardto.CodigoEstado);

            var estadoRequerimiento = await _context.EstadosRequerimientos
                .FirstOrDefaultAsync(e => e.codigoEstado == aprobarRechazardto.CodigoEstado);

            if (estadoHistorialAprobacion == null || estadoRequerimiento == null)
                throw new ArgumentException("El estado proporcionado no existe.");

            var ahora = DateTime.UtcNow;

            var requerimiento = await _context.RequerimientosPersonales
                .FirstOrDefaultAsync(r => r.requerimientoId == aprobarRechazardto.idRequerimiento);

            var historialAprobacionRequerimiento = await _context.HistorialAprobaciones
                .FirstOrDefaultAsync(h => h.requerimientoId == aprobarRechazardto.idRequerimiento);

            if (requerimiento == null || historialAprobacionRequerimiento == null)
                throw new ApplicationException($"No se encontró el requerimiento o el historial de aprobación con ID {aprobarRechazardto.idRequerimiento}.");

            requerimiento.estadoRequerimientoId = estadoRequerimiento.estadoRequerimientoId;
            requerimiento.usuarioUltModId = usuarioUltMod;
            requerimiento.fechaUltMod = ahora;
            requerimiento.fechaFinProceso = ahora;

            historialAprobacionRequerimiento.fechaUltMod = ahora;
            historialAprobacionRequerimiento.usuarioUltModId = usuarioUltMod;
            historialAprobacionRequerimiento.fechaDecision = ahora;
            historialAprobacionRequerimiento.observaciones = aprobarRechazardto.Observaciones;
            historialAprobacionRequerimiento.estadoHistorialAprobacionId = estadoHistorialAprobacion.estadoHistorialAprobacionId;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.RequerimientosPersonales.Update(requerimiento);
                _context.HistorialAprobaciones.Update(historialAprobacionRequerimiento);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return estadoHistorialAprobacion.nombreEstado;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<IEnumerable<ListaEstadosRequerimientoDto>> GetListaEstadosRequerimientoAsync() =>
            await _context.EstadosRequerimientos
            .Where(e => e.esEstadoFinal)
            .Select(e => new ListaEstadosRequerimientoDto
            {
                estadoRequerimientoId = e.estadoRequerimientoId,
                codigoEstado = e.codigoEstado,
                nombreEstado = e.nombreEstado
            })
            .ToListAsync();
    }
}
