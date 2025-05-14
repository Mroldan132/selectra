using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            if(listaCargos == null)
            {
                return NotFound();
            }

            return Ok(listaCargos);
        }
    }
}
