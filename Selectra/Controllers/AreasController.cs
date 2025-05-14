using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
