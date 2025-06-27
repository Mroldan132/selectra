using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Services.Areas;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;
        public AreasController(IAreaService areaService) { 
            _areaService = areaService;
        }

        [HttpGet]
        [Authorize(Roles = "Solicitante,JefeAprobador,RRHH,Administrador")]
        public async Task<IActionResult> GetListaAreas() {
            var listaAreas = await _areaService.GetListaAreasAsync();

            if (listaAreas == null) { 
                return NotFound();
            }

            return Ok(listaAreas);


        }

        [HttpPost("generarArea")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GenerarArea([FromBody] DetalleAreaDto areaDto)
        {
            var areaCreada = await _areaService.GenerarAreaAsync(areaDto);

            if (!areaCreada)
            {
                return BadRequest("Error al generar area");
            }

            return Ok("Area creada satisfactoriamente");
        }
        [HttpPut("actualizar/{idArea}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarArea(int idArea, [FromBody] ActualizarAreaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var areaActualizadaDto = await _areaService.ActualizarAreaAsync(idArea, dto);
                if (areaActualizadaDto == null)
                {
                    return NotFound(new { message = $"Area con ID {idArea} no encontrada o no tiene acceso." });
                }
                return Ok(areaActualizadaDto);
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
                return StatusCode(500, new { message = "Ocurrió un error interno al actualizar el requerimiento." });
            }
        }
    }
}
