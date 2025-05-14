using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.Services.Personales;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalesController : ControllerBase
    {
        private IPersonalesServices _personalesServices;

        public PersonalesController(IPersonalesServices personalesServices)
        {
            _personalesServices = personalesServices;
        }

        [HttpGet("elegiblesComoJefe")]
        [Authorize(Roles = "Solicitante,JefeAprobador,RRHH,Administrador")]
        public async Task<IActionResult> GetListaJefesDirectos()
        {
            var listaJefes = await _personalesServices.GetListaJefesDirectosAsync();

            return Ok(listaJefes);
        }

    }
}
