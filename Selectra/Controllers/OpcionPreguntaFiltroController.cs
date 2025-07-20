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

            if (lista == null || !lista.Any())
                return NotFound(new { message = "No se encontraron opciones de preguntas filtro." });

            return Ok(lista);
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
                    errores = ModelState.Values
                                         .SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var creado = await _opcionPreguntaFiltroService.GenerarOpcionPreguntaFiltroAsync(dto);

                if (!creado)
                    return BadRequest(new { message = "Error al generar la opción de pregunta filtro." });

                return Ok(new { message = "Opción de pregunta filtro creada satisfactoriamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Error interno al crear la opción de pregunta filtro.",
                    detalle = ex.Message
                });
            }
        }

        [HttpPut("actualizar/{idOpcionPreguntaFiltro}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarOpcionPreguntaFiltro(
            int idOpcionPreguntaFiltro,
            [FromBody] ActualizarOpcionPreguntaFiltroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos",
                    errores = ModelState.Values
                                         .SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var actualizado = await _opcionPreguntaFiltroService
                                      .ActualizarOpcionPreguntaFiltroAsync(idOpcionPreguntaFiltro, dto);

                if (!actualizado)
                    return NotFound(new { message = $"La opción con ID {idOpcionPreguntaFiltro} no fue encontrada." });

                return Ok(new { message = "Opción actualizada satisfactoriamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Error interno al actualizar la opción de pregunta filtro.",
                    detalle = ex.Message
                });
            }

        }
        [HttpDelete("eliminar/{idOpcionPreguntaFiltro}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarOpcionPreguntaFiltro(int idOpcionPreguntaFiltro)
        {
            var eliminado = await _opcionPreguntaFiltroService.EliminarOpcionPreguntaFiltroAsync(idOpcionPreguntaFiltro);
            if (!eliminado)
                return NotFound(new { message = $"Opción con ID {idOpcionPreguntaFiltro} no encontrada." });

            return Ok(new { message = "Opción eliminada satisfactoriamente" });
        }
    }
}

