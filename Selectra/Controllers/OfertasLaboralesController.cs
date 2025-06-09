using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.Services.OfertasLaborales;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertasLaboralesController : ControllerBase
    {
        private readonly IOfertasLaboralesServices _ofertasLaboralesServices;
        public OfertasLaboralesController(IOfertasLaboralesServices ofertasLaboralesServices)
        {
            _ofertasLaboralesServices = ofertasLaboralesServices;
        }

        [HttpGet("requerimientosAprobados")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetRequerimientosAprobados()
        {
            
            var requerimientos = await _ofertasLaboralesServices.GetRequerimientosAprobadosAsync();
            
            if(requerimientos == null || !requerimientos.Any())
            {
                return NotFound("No se encontraron requerimientos aprobados.");
            }

            return Ok(requerimientos);
        }
    }
}
