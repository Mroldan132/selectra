using Selectra.DTOs;

namespace Selectra.Services.Vacaciones
{
    public interface ISolicitudVacacionesService
    {

        Task<IEnumerable<SolicitudVacacionesDto>> GetSolicitudesPorPersonalIdAsync(int usuarioId);
        Task<IEnumerable<SolicitudVacacionesDto>> GetSolicitudesPendientesPorAprobadorIdAsync(int aprobadorId);
        Task<(bool Exitoso, string ErrorMessage)> CrearSolicitudAsync(CrearSolicitudVacacionesDto solicitudDto, int usuarioId);
        Task<(bool Exitoso, string ErrorMessage)> AprobarSolicitudAsync(int solicitudId, int aprobadorId);

        Task<(bool Exitoso, string ErrorMessage)> RechazarSolicitudAsync(int solicitudId, int aprobadorId, string motivo);
        Task AcreditarVacacionesAnuales();
    }
}
