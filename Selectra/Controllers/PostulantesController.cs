using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.Services.Postulantes;
using System.Security.Claims;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulantesController : ControllerBase
    {
        private readonly IPostulanteService _postulanteService;

        public PostulantesController(IPostulanteService postulanteService)
        {
            _postulanteService = postulanteService;
        }

        [HttpPut("postularOfertaLaboral/{ofertaId}")]
        public async Task<IActionResult> PostularOfertaLaboral(int ofertaId)
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(usuarioIdStr, out int usuarioId))
            {
                return Forbid("No se pudo identificar al usuario.");
            }

            var postulado = await _postulanteService.PostularOfertaLaboral(ofertaId, int.Parse(usuarioIdStr));

            if(!postulado)
            {
                return BadRequest("No se pudo realizar la postulación. Verifique que la oferta laboral exista y que no haya postulado previamente.");
            }

            return Ok("Postulación exitosa: ");
        }

        [HttpGet("misPostulaciones")]
        public async Task<IActionResult> MisPostulaciones()
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(usuarioIdStr, out int usuarioId))
            {
                return Forbid("No se pudo identificar al usuario.");
            }

            var misOfertas = await _postulanteService.ListaMisOfertasLaborales(usuarioId);

            if (misOfertas == null || misOfertas.Count == 0)
            {
                return NotFound("No se encontraron postulaciones para el usuario.");
            }

            return Ok(misOfertas);
        }
    }
}
