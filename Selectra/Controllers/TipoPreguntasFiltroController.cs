using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.TipoPreguntasFiltro;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPreguntasFiltroController : ControllerBase
    {
        private readonly ITipoPreguntasFiltroService _tipoPreguntasFiltroService;

        public TipoPreguntasFiltroController(ITipoPreguntasFiltroService tipoPreguntasFiltroService)
        {
            _tipoPreguntasFiltroService = tipoPreguntasFiltroService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetListaTipoPreguntasFiltro()
        {
            var listaTipoPreguntasFiltro = await _tipoPreguntasFiltroService.GetListaTipoPreguntasFiltroAsync();
            if (listaTipoPreguntasFiltro == null || !listaTipoPreguntasFiltro.Any())
            {
                return NotFound(new { message = "No se encontraron tipos de preguntas filtro." });
            }
            return Ok(listaTipoPreguntasFiltro);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CrearTipoPreguntasFiltro([FromBody] CrearTipoPreguntasFiltroDto dto)
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

            var result = await _tipoPreguntasFiltroService.CrearTipoPreguntasFiltroAsync(dto);
            if (!result)
            {
                return BadRequest(new { message = "Error al crear el tipo de pregunta filtro." });
            }

            return Ok(new { message = "Tipo de pregunta filtro creado correctamente." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarTipoPreguntasFiltro(int id, [FromBody] ActualizarTipoPreguntasFiltroDto dto)
        {
            if (id != dto.tipoPreguntaId)
            {
                return BadRequest(new { message = "El ID de la URL no coincide con el ID del objeto enviado." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos",
                    errores = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                });
            }

            var actualizarTipo = await _tipoPreguntasFiltroService.ActualizarTipoPreguntasFiltroAsync(dto);
            if (!actualizarTipo)
            {
                return NotFound(new { message = $"No se encontró el tipo de pregunta con ID {id}." });
            }

            return Ok(new { message = "Tipo de pregunta filtro actualizado correctamente." });
        }
    }
}
