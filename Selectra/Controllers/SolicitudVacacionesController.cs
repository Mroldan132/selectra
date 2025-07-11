using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services;
using Selectra.Services.Vacaciones;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudVacacionesController : ControllerBase
    {
        private readonly ISolicitudVacacionesService _solicitudService;

        public SolicitudVacacionesController(ISolicitudVacacionesService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet("mis-solicitudes")]
        public async Task<IActionResult> GetMisSolicitudes()
        {

            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(usuarioIdStr, out int usuarioId))
            {
                return Forbid("No se pudo identificar al usuario.");
            }

            var solicitudes = await _solicitudService.GetSolicitudesPorPersonalIdAsync(int.Parse(usuarioIdStr));
            return Ok(solicitudes);
        }

        [HttpGet("pendientes-aprobacion/{aprobadorId}")]
        public async Task<IActionResult> GetSolicitudesPendientesParaAprobacion(int aprobadorId)
        {
            var solicitudes = await _solicitudService.GetSolicitudesPendientesPorAprobadorIdAsync(aprobadorId);
            return Ok(solicitudes);
        }

        [HttpPost("crearSolicitud")]
        public async Task<IActionResult> CrearSolicitud([FromBody] CrearSolicitudVacacionesDto solicitudDto)
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(usuarioIdStr, out int usuarioId))
            {
                return Forbid("No se pudo identificar al usuario.");
            }
            var resultado = await _solicitudService.CrearSolicitudAsync(solicitudDto, usuarioId);

            if (!resultado.Exitoso)
            {
                return BadRequest(new { message = resultado.ErrorMessage });
            }

            return Ok(new { message = "Solicitud creada exitosamente." });
        }

        [HttpPost("{id}/aprobar/{aprobadorId}")]
        public async Task<IActionResult> AprobarSolicitud(int id, int aprobadorId)
        {
            var resultado = await _solicitudService.AprobarSolicitudAsync(id, aprobadorId);

            if (!resultado.Exitoso)
            {
                return BadRequest(new { message = resultado.ErrorMessage });
            }

            return Ok(new { message = "Solicitud aprobada exitosamente." });
        }



        [HttpPost("{id}/rechazar")]
        public async Task<IActionResult> RechazarSolicitud(int id, [FromBody] RechazarDto rechazoDto)
        {
            var resultado = await _solicitudService.RechazarSolicitudAsync(id, rechazoDto.AprobadorId, rechazoDto.Motivo);

            if (!resultado.Exitoso)
            {
                return BadRequest(new { message = resultado.ErrorMessage });
            }

            return Ok(new { message = "Solicitud rechazada exitosamente." });
        }
    }
}
