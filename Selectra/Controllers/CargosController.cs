using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Services.Cargos;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private readonly ICargosService _cargosService;

        public CargosController(ICargosService cargosService)
        {
            _cargosService = cargosService;
        }

        [HttpGet]
        [Authorize(Roles = "Solicitante,JefeAprobador,RRHH,Administrador")]
        public async Task<IActionResult> GetListaCargos()
        {
            var listaCargos = await _cargosService.GetListaCargosAsync();

            if (listaCargos == null)
            {
                return NotFound();
            }

            return Ok(listaCargos);
        }

        [HttpPost("generarCargo")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GenerarCargo([FromBody] DetalleCargoDto cargoDto)
        {
            var cargoCreado = await _cargosService.GenerarCargoAsync(cargoDto);

            if (!cargoCreado)
            {
                return BadRequest("Error al generar cargo");
            }

            return Ok("Cargo creado satisfactoriamente");
        }

        [HttpPut("actualizar/{idCargo}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarCargo(int idCargo, [FromBody] ActualizarCargoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var actualizado = await _cargosService.ActualizarCargoAsync(idCargo, dto);
                if (!actualizado)
                {
                    return NotFound(new { message = $"Cargo con ID {idCargo} no encontrado o no tiene acceso." });
                }
                return Ok("Cargo actualizado correctamente.");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { message = "Error al guardar en la base de datos. Verifique los datos." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno al actualizar el cargo." });
            }
        }
    }
}
