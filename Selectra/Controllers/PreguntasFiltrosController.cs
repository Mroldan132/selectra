using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.PreguntasFiltros;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasFiltrosController : ControllerBase
    {
        private readonly IPreguntasFiltrosService _preguntasFiltrosService;
        public PreguntasFiltrosController(IPreguntasFiltrosService preguntasFiltrosService)
        {
            _preguntasFiltrosService = preguntasFiltrosService;
        }
        [HttpGet]
        [Authorize (Roles = "Administrador")]
        public async Task<IActionResult> GetListaPreguntasFiltros()
        {
            var lista = await _preguntasFiltrosService.GetListaPreguntasFiltrosAsync();

            if (lista == null || !lista.Any())
                return NotFound(new { message = "No se encontraron preguntas filtros." });

            return Ok(lista);
        }


        [HttpPost("generarPreguntaFiltro")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GenerarPreguntaFiltro([FromBody] DetallePreguntasFiltrosDto preguntaFiltroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos",
                    errores = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                });
            }

            var preguntaFiltroCreada = await _preguntasFiltrosService.GenerarPreguntaFiltroAsync(preguntaFiltroDto);

            if (!preguntaFiltroCreada)
                return BadRequest(new { message = "Error al generar la pregunta filtro." });

            return Ok(new { message = "Pregunta filtro creada satisfactoriamente." });
        } 

        [HttpPut("actualizar/{idPreguntaFiltro}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarPreguntaFiltro( int idPreguntaFiltro,[FromBody] ActualizarPreguntasFiltrosDto preguntaFiltroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos",
                    errores = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var actualizado = await _preguntasFiltrosService.ActualizarPreguntaFiltroAsync(idPreguntaFiltro, preguntaFiltroDto);

                if (!actualizado)
                {
                    return NotFound(new { message = $"La pregunta filtro con ID {idPreguntaFiltro} no fue encontrada." });
                }

                return Ok(new { message = "Pregunta filtro actualizada satisfactoriamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Error interno al actualizar la pregunta filtro",
                    detalle = ex.Message
                });
            }
        }

        [HttpDelete("eliminar/{idPreguntaFiltro}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarPreguntaFiltro(int idPreguntaFiltro)
        {
            try
            {
                var eliminado = await _preguntasFiltrosService.EliminarPreguntaFiltroAsync(idPreguntaFiltro);

                if (!eliminado)
                {
                    return NotFound(new { message = $"La pregunta filtro con ID {idPreguntaFiltro} no fue encontrada." });
                }

                return Ok(new { message = "Pregunta filtro eliminada satisfactoriamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Error interno al eliminar la pregunta filtro",
                    detalle = ex.Message
                });
            }
        }

    }
}
