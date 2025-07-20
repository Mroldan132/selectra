using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.Aspirantes;
using Selectra.Services.Personales;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspirantesController : ControllerBase
    {
        private readonly IAspirantesService _aspirantesServices;

        public AspirantesController(IAspirantesService aspirantesServices)
        {
            _aspirantesServices = aspirantesServices;
        }

        [HttpGet("listaAspirantes")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetListaAspirantes()
        {
            var listaAspirantes = await _aspirantesServices.GetListaAspirantesAsync();
            return Ok(listaAspirantes);

        }

        [HttpGet("detalleAspirante/{aspiranteId}")]
        [Authorize(Roles = "Administrador,Aspirante")]
        public async Task<IActionResult> DetalleAspirante(int aspiranteId)
        {
            var aspirante = await _aspirantesServices.GetDetalleAspiranteAsync(aspiranteId);

            if (aspirante == null)
            {
                return BadRequest("Error en traer los datos");
            }

            return Ok(aspirante);
        }
    }
}
