using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.OfertasLaborales;

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

    }
}
