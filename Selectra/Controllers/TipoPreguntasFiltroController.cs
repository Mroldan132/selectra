using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.TipoPreguntasFiltro;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")] //  Autorización global para todos los endpoints
    public class TipoPreguntasFiltroController : ControllerBase
    {
        private readonly ITipoPreguntasFiltroService _tipoPreguntasFiltroService;

        public TipoPreguntasFiltroController(ITipoPreguntasFiltroService tipoPreguntasFiltroService)
        {
            _tipoPreguntasFiltroService = tipoPreguntasFiltroService;
        }

        // GET: api/TipoPreguntasFiltro
        [HttpGet]
        public async Task<IActionResult> GetListaTipoPreguntasFiltro()
        {
            var listaTipoPreguntasFiltro = await _tipoPreguntasFiltroService.GetListaTipoPreguntasFiltroAsync();

           
            return Ok(listaTipoPreguntasFiltro);
        }

        // GET: api/TipoPreguntasFiltro/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTipoPreguntasFiltroPorId(int id)
        {
            var tipo = await _tipoPreguntasFiltroService.GetTipoPreguntasFiltroPorIdAsync(id);
            if (tipo == null)
            {
                return NotFound(new { message = $"No se encontró el tipo de pregunta con ID {id}." });
            }

            return Ok(tipo);
        }

        //POST: api/TipoPreguntasFiltro
        [HttpPost]
        public async Task<IActionResult> CrearTipoPreguntasFiltro([FromBody] CrearTipoPreguntasFiltroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos",
                    errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var result = await _tipoPreguntasFiltroService.CrearTipoPreguntasFiltroAsync(dto);
            if (!result)
            {
                return BadRequest(new { message = "Error al crear el tipo de pregunta filtro." });
            }

            return Ok(new { message = "Tipo de pregunta filtro creado correctamente." });
        }

        // PUT: api/TipoPreguntasFiltro/{id}
        [HttpPut("{id:int}")]
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
                    errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var actualizarTipo = await _tipoPreguntasFiltroService.ActualizarTipoPreguntasFiltroAsync(dto);
            if (!actualizarTipo)
            {
                return NotFound(new { message = $"No se encontró el tipo de pregunta con ID {id}." });
            }

            return Ok(new { message = "Tipo de pregunta filtro actualizado correctamente." });
        }

        // DELETE: api/TipoPreguntasFiltro/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarTipoPreguntasFiltro(int id)
        {
            var eliminado = await _tipoPreguntasFiltroService.EliminarTipoPreguntasFiltroAsync(id);
            if (!eliminado)
            {
                return NotFound(new { message = $"No se encontró el tipo de pregunta con ID {id}." });
            }

            return Ok(new { message = "Tipo de pregunta filtro eliminado correctamente." });
        }
    }
}
