using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Services.TipoDocumento;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDocumentosController : ControllerBase
    {
        private readonly ITiposDocumentosService _tiposDocumentosService;
        public TiposDocumentosController(ITiposDocumentosService tiposDocumentosService)
        {
            _tiposDocumentosService = tiposDocumentosService;
        }

        [HttpGet]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetListaTiposDocumentos()
        {
           var listaTiposDocumentos = await _tiposDocumentosService.GetListaTiposDocumentosAsync();
            if (listaTiposDocumentos == null || !listaTiposDocumentos.Any())
            {
                return NotFound("No se encontraron tipos de documentos.");
            }
            return Ok(listaTiposDocumentos);
        }

        [HttpGet("{tipoDocumentoId}")]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetTiposDocumentosById(int tipoDocumentoId)
        {
            var tipodocumento = await _tiposDocumentosService.GetTiposDocumentosAsync(tipoDocumentoId);

            if (tipodocumento == null)
                return NotFound("no se encontro el tipo documento");

            return Ok(tipodocumento);
        }


        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CrearTiposDocumentos([FromBody] ListaTiposDocumentoDto dto)
        {
            if (dto == null)
                return BadRequest("no se mando el objeto");

            var nuevotipodocumento = await _tiposDocumentosService.CrearTiposDocumentosAsync(dto);

            if (!nuevotipodocumento)
            {
                return BadRequest("error al crear el tipo de documento.");
            }

            return Ok("tipo de documento creado exitosamente.");
        }

        [HttpPut]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarTiposDocumentos([FromBody] ListaTiposDocumentoDto dto)
        {

            if (dto == null)
                return BadRequest("no se mando el objeto");

            var actualizartipodocumento = await _tiposDocumentosService.ActualizarTiposDocumentos(dto);

            if (!actualizartipodocumento)
                return BadRequest("error al actualizar el tipo de documento.");

            return Ok("tipo de documento actualizado exitosamente.");
        }


        //[httpput("{id}")]
        ////[authorize(roles = "administrador")]
        //public async task<iactionresult> editartiposdocumentos(int id, [frombody] listatiposdocumentodto dto)
        //{
        //    if (dto == null)
        //        return badrequest("el objeto no puede ser nulo.");

        //    var resultado = await _tiposdocumentosservice.editartiposdocumentosasync(dto, id);

        //    if (!resultado)
        //        return notfound($"no se pudo editar el tipo de documento con id {id}.");

        //    return ok("tipo de documento actualizado exitosamente.");
        //}
    }
}
