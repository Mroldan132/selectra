using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services.NivelAcademicos;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelAcademicosController : ControllerBase
    {
        private readonly INivelAcademicosService _nivelAcademicosService;
        public NivelAcademicosController(INivelAcademicosService nivelAcademicosService)
        {
            _nivelAcademicosService = nivelAcademicosService;
        }
        [HttpGet]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> GetListaNivelAcademicos()
        {
            var listaNivelAcademicos = await _nivelAcademicosService.GetListaNivelAcademicosAsync();
            if (listaNivelAcademicos == null || !listaNivelAcademicos.Any())
            {
                return NotFound("No se encontraron niveles académicos.");
            }
            return Ok(listaNivelAcademicos);
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> CrearNivelAcademicos([FromBody] ListaNivelAcademicosDto dto)
        {
            if (dto == null)
            
             return BadRequest("Datos inválidos para crear un nivel académico.");
          
            var resultado = await _nivelAcademicosService.CrearNivelAcademicosAsync(dto);
            if (!resultado)
            {
                return BadRequest("Error al crear el nivel académico.");
            }
            return Ok("Se creo correctamente el nivel");
        }
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarNivelAcademicos([FromBody] ListaNivelAcademicosDto dto)
        {
            if (dto ==null)
            
                return BadRequest("Datos inválidos para actualizar un nivel académico.");
            
            var actualizarnivel = await _nivelAcademicosService.ActualizarNivelAcademicosAsync(dto);
            if (!actualizarnivel)
            {
                return BadRequest("Error al actualizar el nivel académico.");
            }
            return Ok("Se actualizo correctamente el nivel");
        }

    }
}
