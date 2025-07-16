using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.OpcionPreguntaFiltro;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpcionPreguntaFiltroController : ControllerBase
    {
        private readonly IOpcionPreguntaFiltroService _opcionPreguntaFiltroService;

        public OpcionPreguntaFiltroController(IOpcionPreguntaFiltroService opcionPreguntaFiltroService)
        {
            _opcionPreguntaFiltroService = opcionPreguntaFiltroService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetListaOpcionPreguntaFiltro()
        {
            var lista = await _opcionPreguntaFiltroService.GetListaOpcionPreguntaFiltroAsync();
            return Ok(lista); // Devuelve lista vacía si no hay datos
        }

        [HttpPost("generarOpcionPreguntaFiltro")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GenerarOpcionPreguntaFiltro([FromBody] DetalleOpcionPreguntaFiltroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos",
                    errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var creado = await _opcionPreguntaFiltroService.GenerarOpcionPreguntaFiltroAsync(dto);
            if (!creado)
            {
                return BadRequest(new { message = "Error al generar la opción de pregunta filtro" });
            }

            return Ok(new { message = "Opción de pregunta filtro creada satisfactoriamente" });
        }

        [HttpPut("actualizar/{idOpcionPreguntaFiltro}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarOpcionPreguntaFiltro(int idOpcionPreguntaFiltro, [FromBody] ActualizarOpcionPreguntaFiltroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos",
                    errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var actualizado = await _opcionPreguntaFiltroService.ActualizarOpcionPreguntaFiltroAsync(idOpcionPreguntaFiltro, dto);
                if (!actualizado)
                {
                    return NotFound(new { message = $"Opción con ID {idOpcionPreguntaFiltro} no encontrada." });
                }

                return Ok(new { message = "Opción actualizada satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Error interno al actualizar la opción de pregunta filtro",
                    detalle = ex.Message
                });
            }
        }
    }
}
