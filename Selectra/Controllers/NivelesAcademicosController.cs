using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.Services.NivelesAcademicos;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelesAcademicosController : ControllerBase
    {

        private readonly INivelesAcademicosService _nivelesAcademicosService;

        public NivelesAcademicosController(INivelesAcademicosService nivelesAcademicosService)
        {
            _nivelesAcademicosService = nivelesAcademicosService;
        }

        [HttpGet("ListaNivelesAcademicos")]
        public async Task<IActionResult> GetListaNivelesAcademicos()
        {
            var nivelesAcademicos = await _nivelesAcademicosService.GetListaNivelesAcademicosAsync();
            return Ok(nivelesAcademicos);

        }
    }
}
