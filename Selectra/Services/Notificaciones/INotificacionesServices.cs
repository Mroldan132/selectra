using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Notificaciones
{
    public interface INotificacionesServices
    {
        Task<bool> CrearNotificacionAsync(CrearNotificacionDto notificacion);
        Task<NotificacionesResponseDto> GetListNotificacionesAsync(int usuarioId);
        Task<bool> LeerNotificiacionAsync(int notificacionId, int usuarioId);
        Task<bool> MarcarTodasLeidasAsync(int usuarioId); 
        Task<bool> CrearNotificacionNuevoRequerimientoAsync(RequerimientoCreadoDto requerimientoCreadoDto);
        Task<bool> CrearNotificacionRequerimientoAprobadoRechazado(AprobarRechazarRequerimientoDto aprobarRechazardto);
    }
}
