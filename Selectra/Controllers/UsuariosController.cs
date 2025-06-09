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
        public UsuariosController(IUsuarioService usuarioService, SelectraContext context)
        {
            _usuarioService = usuarioService;
            _context = context;

        }

        [HttpPost("registrarAdministrador")]
        public async Task<IActionResult> RegistrarAdministrador([FromBody] RegistrarAdministradorDto registroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevoUsuario = await _usuarioService.RegistrarAdministradorAsync(registroDto, 1);
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

        [HttpPost("registrarPersonal")]
        [Authorize(Roles = "Administrador,RRHH")]
        public async Task<IActionResult> RegistrarPersonal([FromBody] RegistrarPersonalDto registroDto)
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
                var nuevoUsuario = await _usuarioService.RegistrarPersonalAsync(registroDto, 1);
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

        [HttpPost("registrarAspirante")]
        public async Task<IActionResult> RegistrarAspirante([FromBody] RegistrarAspiranteDto registroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _context.Roles.AnyAsync(r => r.rolId == 4))
                return BadRequest(new { message = $"El Rol Aspirante no es válido." });

            try
            {
                var nuevoUsuario = await _usuarioService.RegistrarAspiranteAsync(registroDto, 1);
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

        [HttpGet("listaRoles")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetListaRoles()
        {
            var roles = await _usuarioService.GetListaRolesAync();
            return Ok(roles);
        }

        [HttpGet("verificar-existencia/{codUsuario}")] 
        public async Task<IActionResult> VerificarExisteUsuario(string codUsuario)
        {
            var existe = await _usuarioService.VerificarExisteUsuario(codUsuario);

            return Ok(existe);
        }

        [HttpPut("actualizarPersonal/{personalId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> actualizarPersonal([FromBody] ActualizarPersonalDto personalDto, int personalId)
        {
            var actualizado = await _usuarioService.ActualizarPersonal(personalDto, personalId,1);

            if(!actualizado)
            {
                return BadRequest(new { message = "Error al actualizar el personal." });
            }

            return Ok(actualizado);
        }
    }
}
