using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
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

        [HttpGet("listaPersonal")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetListaPersonal()
        {
            var listaPersonal = await _personalesServices.GetListaPersonalessAsync();
            return Ok(listaPersonal);

        }

        [HttpGet("detallePersonal/{personalId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DetallePersonal(int personalId)
        {
            var personal = await _personalesServices.GetDetallePersonalAsync(personalId);

            if(personal == null) {
                return BadRequest("Error en traer los datos");
            }

            return Ok(personal);
        }
        [HttpGet("organigrama")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListaPersonalOrganigrama()
        {
            var organigrama = await _personalesServices.ListaPersonalOrganigrama();
            return Ok(organigrama);
        }



        [HttpGet("resumenPorArea")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetResumenPorArea()
        {
            var resumen = await _personalesServices.GetPersonalesPorAreaAsync();
            return Ok(resumen);
        }

        [HttpGet("resumenPorCargo")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetResumenPorCargo()
        {
            var resumen = await _personalesServices.GetPersonalesPorCargoAsync();
            return Ok(resumen);
        }


    }
}
