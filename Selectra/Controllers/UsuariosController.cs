using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;
using Selectra.Services.Usuarios;
using System.Security.Claims;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly SelectraContext _context;
        public UsuariosController(IUsuarioService usuarioService,SelectraContext context)
        {
            _usuarioService = usuarioService;
            _context = context;

        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto registroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.Roles.AnyAsync(r => r.rolId == registroDto.RolId))
                return BadRequest(new { message = $"El RolId '{registroDto.RolId}' no es válido." });
            if (!await _context.Areas.AnyAsync(a => a.areaId == registroDto.AreaId))
                return BadRequest(new { message = $"El AreaId '{registroDto.AreaId}' no es válida." });

            try
            {
                var nuevoUsuario = await _usuarioService.RegistrarUsuarioAsync(registroDto, 1);
                return CreatedAtAction(nameof(GetUsuarioPorId), new { id = nuevoUsuario.usuarioId }, new { nuevoUsuario.usuarioId, nuevoUsuario.codUsuario });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "Ocurrió un error interno al registrar el usuario." });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,RRHH")]
        public async Task<IActionResult> GetUsuarioPorId(int id)
        {
            var usuarioDto = await _usuarioService.GetUsuarioPorIdAsync(id); 

            if (usuarioDto == null)
            {
                return NotFound(new { message = $"Usuario con ID {id} no encontrado." });
            }

            return Ok(usuarioDto);
        }

    }
}
