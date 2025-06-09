using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.Services.DatosPersonales;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosPersonalesController : ControllerBase
    {
        private readonly IDatosPersonalesService _datosPersonalesService;
        public DatosPersonalesController(IDatosPersonalesService datosPersonalesService)
        {
            _datosPersonalesService = datosPersonalesService;
        }
        [HttpGet("ListaTiposDocumento")]
        public async Task<IActionResult> GetListaTiposDocumento()
        {
            var tiposDocumento = await _datosPersonalesService.GetListaTiposDocumentoAsync();
            return Ok(tiposDocumento);
        }

        [HttpGet("ListaDepartamentos")]
        public async Task<IActionResult> GetListaDepartamentos()
        {
            var departamentos = await _datosPersonalesService.GetListaDepartamentosAsync();
            return Ok(departamentos);
        }

        [HttpGet("ListaProvincias/{departamentoId}")]
        public async Task<IActionResult> GetListaProvincias(string departamentoId)
        {
            var provincias = await _datosPersonalesService.GetListaProvinciasAsync(departamentoId);
            return Ok(provincias);
        }

        [HttpGet("ListaDistritos/{provinciaId}")]
        public async Task<IActionResult> GetListaDistritos(string provinciaId)
        {
            var distritos = await _datosPersonalesService.GetListaDistritosAsync(provinciaId);
            return Ok(distritos);
        }
    }
}
