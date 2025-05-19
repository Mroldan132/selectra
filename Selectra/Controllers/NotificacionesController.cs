using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Selectra.Services.Notificaciones;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionesServices _notificacionesServices;

        public NotificacionesController(INotificacionesServices notificacionesServices)
        {
            _notificacionesServices = notificacionesServices;
        }

        [HttpGet("GetListNotificaciones/{usuarioId}")]
        public async Task<IActionResult> GetListNotificaciones(int usuarioId)
        {
            var notificaciones = await _notificacionesServices.GetListNotificacionesAsync(usuarioId);
            if (notificaciones == null)
            {
                return NotFound("No hay notificaciones para el usuario.");
            }
            return Ok(notificaciones);
        }

        [HttpPut("LeerNotificiacion/{notificacionId}/{usuarioId}")]
        public async Task<IActionResult> LeerNotificiacion(int notificacionId, int usuarioId)
        {
            var result = await _notificacionesServices.LeerNotificiacionAsync(notificacionId, usuarioId);
            if (!result)
            {
                return NotFound("Notificación no encontrada o ya leída.");
            }
            return Ok("Notificación marcada como leída.");
        }

        [HttpPut("MarcarTodasLeidas/{usuarioId}")]
        public async Task<ActionResult> MarcarTodasLeidas(int usuarioId)
        {
            var result = await _notificacionesServices.MarcarTodasLeidasAsync(usuarioId);

            if (!result)
            {
                return NotFound("Notificaciones no encontradas o ya leidas");
            }
            return Ok("Todas las notificaciones leidas");
        }


    }
}
