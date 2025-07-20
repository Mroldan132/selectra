using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.OfertasLaborales;
using System.Security.Claims;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertasLaboralesController : ControllerBase
    {
        private readonly IOfertasLaboralesServices _ofertasLaboralesServices;
        public OfertasLaboralesController(IOfertasLaboralesServices ofertasLaboralesServices)
        {
            _ofertasLaboralesServices = ofertasLaboralesServices;
        }

        [HttpGet("requerimientosAprobados")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetRequerimientosAprobados()
        {

            var requerimientos = await _ofertasLaboralesServices.GetRequerimientosAprobadosAsync();

            if (requerimientos == null || !requerimientos.Any())
            {
                return NotFound("No se encontraron requerimientos aprobados.");
            }

            return Ok(requerimientos);
        }

        [HttpGet("crearOfertaLaboralRequerimiento/{requerimientoId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CrearOfertaLaboralRequerimiento(int requerimientoId)
        {
            var ofertaLaborarGenerado = await _ofertasLaboralesServices.CrearOfertaLaboralRequerimiento(requerimientoId);

            if (ofertaLaborarGenerado == null)
            {
                return NotFound("No se encontro un requerimiento con este id");
            }

            return Ok(ofertaLaborarGenerado);
        }

        [HttpPost("generarOfertaLaboral")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GenerarOfertaLaboral([FromBody] DetalleOfertaLaboralDto ofertaLaboralDto) {
            var ofertaCreada = await _ofertasLaboralesServices.GenerarOfertaLaborarAsync(ofertaLaboralDto, 1);

            if (!ofertaCreada)
            {
                return BadRequest("Error al generar oferta laboral");
            }

            return Ok("Oferta creada satisfactoriamente");
        }

        [HttpPut("actualizarOfertaLaboral")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarOfertaLaboral([FromBody] DetalleOfertaLaboralDto ofertaLaboralDto)
        {
            var ofertaCreada = await _ofertasLaboralesServices.ActualizarOfertaLaborarAsync(ofertaLaboralDto, 1);

            if (!ofertaCreada)
            {
                return BadRequest("Error al actualizar oferta laboral");
            }

            return Ok("Oferta actualizada satisfactoriamente");
        }

        [HttpGet("listaOfertasLaborales")]
        public async Task<IActionResult> ListaOfertasLaborales()
        {
            var ofertasLaborales = await _ofertasLaboralesServices.GetListOfertasLaboralesAsync();

            if(ofertasLaborales == null || !ofertasLaborales.Any()) {
                return NotFound("Error al traer los datos");
            }

            return Ok(ofertasLaborales);
        }

        [HttpGet("detalleOfertaLaboral/{ofertaId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DetalleOfertaLaboral(int ofertaId)
        {
            var ofertaLaboral = await _ofertasLaboralesServices.DetalleOfertaLaboralRequerimientoAsync(ofertaId);

            if (ofertaLaboral == null)
            {
                return NotFound("No se encontro una oferta con este id");
            }

            return Ok(ofertaLaboral);
        }


        [HttpGet("listaOfertasPublicadas")]
        public async Task<IActionResult> ListaOfertasPublicadas()
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(usuarioIdStr, out int usuarioId))
            {
                return Forbid("No se pudo identificar al usuario.");
            }
            var ofertasPublicadas = await _ofertasLaboralesServices.ListaOfertasPublicadas(usuarioId);
            if (ofertasPublicadas == null || !ofertasPublicadas.Any())
            {
                return NotFound("No se encontraron ofertas publicadas para este usuario.");
            }

            return Ok(ofertasPublicadas);
        }

        [HttpPut("pasarSiguienteEstadoOferta/{ofertaLaboralId}")]
        public async Task<IActionResult> PasarSiguienteEstadoOferta(int ofertaLaboralId)
        {
            var ofertaActualizada = await _ofertasLaboralesServices.PasarSiguienteEstadoOferta(ofertaLaboralId);

            if (!ofertaActualizada)
            {
                return BadRequest("Error al pasar la oferta al siguiente estado. Verifique que la oferta exista y que no haya sido procesada previamente.");
            }
            return Ok("Oferta laboral actualizada satisfactoriamente.");
        }


    }
}
