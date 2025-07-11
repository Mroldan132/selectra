using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;
using Selectra.Services.Vacaciones;

public class SolicitudVacacionesService : ISolicitudVacacionesService
{
    private readonly SelectraContext _context;

    private const int ESTADO_PENDIENTE = 1;
    private const int ESTADO_APROBADA = 2;
    private const int ESTADO_RECHAZADA = 3;

    public SolicitudVacacionesService(SelectraContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SolicitudVacacionesDto>> GetSolicitudesPorPersonalIdAsync(int usuarioId)
    {
        var personal = await _context.Personales
            .Include(i => i.DatosPersonales)
            .FirstOrDefaultAsync(i => i.DatosPersonales.usuarioId == usuarioId);

        return await _context.SolicitudVacaciones
            .Where(s => s.personalId == personal.personalId)
            .Include(s => s.Estado)
            .OrderByDescending(s => s.FechaCreacion)
            .Select(s => new SolicitudVacacionesDto
            {
                Id = s.id,
                FechaInicio = s.FechaInicio,
                FechaFin = s.FechaFin,
                DiasSolicitados = (s.FechaFin - s.FechaInicio).TotalDays + 1,
                FechaCreacion = s.FechaCreacion,
                Estado = s.Estado.Nombre,
                ComentariosEmpleado = s.ComentariosEmpleado,
                ComentariosAprobador = s.ComentariosAprobador
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<SolicitudVacacionesDto>> GetSolicitudesPendientesPorAprobadorIdAsync(int aprobadorId)
    {
        return await _context.SolicitudVacaciones
            .Where(s => s.AprobadorId == aprobadorId && s.estadoId == ESTADO_PENDIENTE)
            .Include(s => s.Personal) 
            .Include(s => s.Estado)
            .OrderBy(s => s.FechaCreacion)
            .Select(s => new SolicitudVacacionesDto
            {
                Id = s.id,
                NombreEmpleado = s.Personal.DatosPersonales.nombres + " " + s.Personal.DatosPersonales.apellidoPaterno,
                FechaInicio = s.FechaInicio,
                FechaFin = s.FechaFin,
                DiasSolicitados = (s.FechaFin - s.FechaInicio).TotalDays + 1,
                FechaCreacion = s.FechaCreacion,
                Estado = s.Estado.Nombre,
                ComentariosEmpleado = s.ComentariosEmpleado
            })
            .ToListAsync();
    }

    public async Task<(bool Exitoso, string ErrorMessage)> CrearSolicitudAsync(CrearSolicitudVacacionesDto solicitudDto, int usuarioId)
    {
        var empleado = await _context.Personales
            .Include(i => i.DatosPersonales)
            .FirstOrDefaultAsync(i => i.DatosPersonales.usuarioId == usuarioId);

        if (empleado == null)
        {
            return (false, "El empleado no fue encontrado.");
        }

        if (solicitudDto.FechaInicio > solicitudDto.FechaFin)
        {
            return (false, "La fecha de inicio no puede ser posterior a la fecha de fin.");
        }

        var diasSolicitados = (decimal)(solicitudDto.FechaFin - solicitudDto.FechaInicio).TotalDays + 1;

        if (diasSolicitados > empleado.DiasVacacionesDisponibles)
        {
            return (false, $"No tiene suficientes días disponibles. Solicita {diasSolicitados} y tiene {empleado.DiasVacacionesDisponibles}.");
        }

        if (empleado.jefeDirectoId == null)
        {
            return (false, "No tiene un jefe directo asignado para aprobar la solicitud.");
        }

        var nuevaSolicitud = new SolicitudVacaciones
        {
            personalId = empleado.personalId,
            FechaInicio = solicitudDto.FechaInicio,
            FechaFin = solicitudDto.FechaFin,
            ComentariosEmpleado = solicitudDto.ComentariosEmpleado,
            ComentariosAprobador = "",
            FechaCreacion = DateTime.Now,
            estadoId = ESTADO_PENDIENTE,
            AprobadorId = empleado.jefeDirectoId
        };

        _context.SolicitudVacaciones.Add(nuevaSolicitud);
        await _context.SaveChangesAsync();

        return (    true, (string?)null);
    }

    public async Task<(bool Exitoso, string ErrorMessage)> AprobarSolicitudAsync(int solicitudId, int aprobadorId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var solicitud = await _context.SolicitudVacaciones
                .Include(s => s.Personal) 
                .FirstOrDefaultAsync(s => s.id == solicitudId);

            if (solicitud == null) return (false, "Solicitud no encontrada.");
            if (solicitud.AprobadorId != aprobadorId) return (false, "No tiene permisos para aprobar esta solicitud.");
            if (solicitud.estadoId != ESTADO_PENDIENTE) return (false, "Esta solicitud ya ha sido procesada.");

            var diasSolicitados = (decimal)(solicitud.FechaFin - solicitud.FechaInicio).TotalDays + 1;

            solicitud.estadoId = ESTADO_APROBADA;

            solicitud.Personal.DiasVacacionesDisponibles -= diasSolicitados;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();


            return (true, null);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, "Ocurrió un error inesperado al aprobar la solicitud.");
        }
    }

    public async Task<(bool Exitoso, string ErrorMessage)> RechazarSolicitudAsync(int solicitudId, int aprobadorId, string motivo)
    {
        var solicitud = await _context.SolicitudVacaciones.FindAsync(solicitudId);

        if (solicitud == null) return (false, "Solicitud no encontrada.");
        if (solicitud.AprobadorId != aprobadorId) return (false, "No tiene permisos para rechazar esta solicitud.");
        if (solicitud.estadoId != ESTADO_PENDIENTE) return (false, "Esta solicitud ya ha sido procesada.");
        if (string.IsNullOrWhiteSpace(motivo)) return (false, "Debe proporcionar un motivo para el rechazo.");

        solicitud.estadoId = ESTADO_RECHAZADA;
        solicitud.ComentariosAprobador = motivo;

        await _context.SaveChangesAsync();


        return (true, null);
    }
    public async Task AcreditarVacacionesAnuales()
    {
        var empleadosActivos = await _context.Personales
            .Where(p => p.activo && p.fechaIngresoCompania.HasValue)
            .ToListAsync();

        foreach (var empleado in empleadosActivos)
        {
            var fechaIngreso = empleado.fechaIngresoCompania.Value;
            var hoy = DateTime.Today;

            var proximoAniversario = fechaIngreso.AddYears(hoy.Year - fechaIngreso.Year);
            if (proximoAniversario < hoy)
            {
                proximoAniversario = proximoAniversario.AddYears(1);
            }

            var ultimoAniversario = proximoAniversario.AddYears(-1);

            if (ultimoAniversario.Year >= fechaIngreso.Year &&
               (empleado.FechaUltimaAcreditacionVacaciones == null || empleado.FechaUltimaAcreditacionVacaciones.Value < ultimoAniversario))
            {
                empleado.DiasVacacionesDisponibles += 30;
                empleado.FechaUltimaAcreditacionVacaciones = hoy;
            }
        }

        await _context.SaveChangesAsync();
    }
}