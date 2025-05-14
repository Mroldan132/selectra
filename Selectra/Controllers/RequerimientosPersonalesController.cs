using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;
using Selectra.Services.Requerimiento;
using System.Security.Claims;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequerimientosPersonalesController : ControllerBase
    {
        private readonly IRequerimientoPersonalService _requerimientoService;
        private readonly SelectraContext _context;

        public RequerimientosPersonalesController(IRequerimientoPersonalService requerimientoService, SelectraContext context)
        {
            _requerimientoService = requerimientoService;
            _context = context;
        }

        [HttpGet("misSolicitudes")]
        [Authorize(Roles = "Solicitante,JefeAprobador")]
        public async Task<IActionResult> GetMisSolicitudes()
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(usuarioIdStr, out int usuarioId))
            {
                return Forbid("No se pudo identificar al usuario.");
            }

            var personalSolicitante = await _context.Personales.FirstOrDefaultAsync(p => p.usuarioId == usuarioId);
            if (personalSolicitante == null)
            {
                return Ok(personalSolicitante);
                //return NotFound(new { message = "El usuario actual no tiene un registro de personal asociado." });
            }

            try
            {
                var misRequerimientos = await _requerimientoService.GetMisRequerimientosAsync(personalSolicitante.personalId);
                if (misRequerimientos == null || !misRequerimientos.Any())
                {
                    return Ok(new List<MisRequerimientosListDto>());
                }
                return Ok(misRequerimientos);
            }
            catch (Exception ex)
            {
                // Log ex
                return StatusCode(500, new { message = "Ocurrió un error interno al obtener sus requerimientos." });
            }
        }

        [HttpPost("crear")]
        [Authorize(Roles = "Solicitante,JefeAprobador,RRHH,Administrador")]
        public async Task<IActionResult> CrearRequerimiento([FromBody] CrearRequerimientoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.TiposRequerimientos.AnyAsync(tr => tr.tipoRequerimientoId == dto.TipoRequerimientoId))
                return BadRequest(new { message = $"El TipoRequerimientoId '{dto.TipoRequerimientoId}' no es válido." });
            if (!await _context.Areas.AnyAsync(a => a.areaId == dto.AreaId))
                return BadRequest(new { message = $"El AreaId '{dto.AreaId}' no es válida." });
            if (!await _context.Cargos.AnyAsync(c => c.cargoId == dto.CargoId))
                return BadRequest(new { message = $"El CargoId '{dto.CargoId}' no es válida." });
            if (dto.JefeDestinoId.HasValue && !await _context.Personales.AnyAsync(p => p.personalId == dto.JefeDestinoId.Value))
                return BadRequest(new { message = $"El JefeDestinoId '{dto.JefeDestinoId.Value}' no es válido." });

            var solicitanteIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(solicitanteIdStr, out int usuarioSolicitanteId))
            {
                return Forbid("No se pudo identificar al usuario solicitante.");
            }

            var personalSolicitante = await _context.Personales.FirstOrDefaultAsync(p => p.usuarioId == usuarioSolicitanteId);
            if (personalSolicitante == null)
            {
                return BadRequest(new { message = "El usuario solicitante no tiene un registro de personal asociado." });
            }

            try
            {
                var requerimientoCreadoDto = await _requerimientoService.CrearRequerimientoAsync(dto, personalSolicitante.personalId, usuarioSolicitanteId);

                return CreatedAtAction(nameof(GetRequerimientoPorId), new { id = requerimientoCreadoDto.RequerimientoId }, requerimientoCreadoDto);

            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new { message = "Error al guardar en la base de datos. Verifique los datos." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno al crear el requerimiento." });
            }
        }

        [HttpGet("{id}", Name = "GetRequerimientoPorId")] 
        [Authorize(Roles = "Solicitante,JefeAprobador,RRHH,Administrador")]
        public async Task<IActionResult> GetRequerimientoPorId(int id)
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(usuarioIdStr, out int usuarioId))
            {
                return Forbid("No se pudo identificar al usuario.");
            }

            var personalActual = await _context.Personales.FirstOrDefaultAsync(p => p.usuarioId == usuarioId);
            if (personalActual == null)
            {
                return BadRequest(new { message = "El usuario actual no tiene un registro de personal asociado." });
            }

            try
            {
                var requerimientoDto = await _requerimientoService.GetRequerimientoDetalleAsync(id, personalActual.personalId);

                if (requerimientoDto == null)
                {
                    return NotFound(new { message = $"Requerimiento con ID {id} no encontrado o no tiene acceso." });
                }

                return Ok(requerimientoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ocurrió un error interno al obtener el detalle del requerimiento {id}." });
            }
        }


        [HttpGet("ListaTiposRequerimiento")]
        [Authorize(Roles = "Solicitante,JefeAprobador,RRHH,Administrador")]
        public async Task<IActionResult> GetListaTiposRequerimientos()
        {
            var listaTiposRequerimientos = await _requerimientoService.GetListaTiposRequerimientosAsync();

            if(listaTiposRequerimientos == null)
            {
                return NoContent();
            }

            return Ok(listaTiposRequerimientos);
        }
    }
}
