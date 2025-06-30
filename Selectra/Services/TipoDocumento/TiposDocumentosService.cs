using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.TipoDocumento
{
    public class TiposDocumentosService : ITiposDocumentosService
    {
        private readonly SelectraContext _context;

        public TiposDocumentosService(SelectraContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListaTiposDocumentoDto>> GetListaTiposDocumentosAsync()
        {
            return await _context.TiposDocumentos
                .Select(td => new ListaTiposDocumentoDto
                {
                    tipoDocumentoId = td.tipoDocumentoId,
                    nombreTipoDocumento = td.nombreTipoDocumento
                })
                .ToListAsync();
        }

        public async Task<bool> CrearTiposDocumentosAsync(ListaTiposDocumentoDto dto)
        {
            var tipodocumento = new Models.TipoDocumento
            {
                nombreTipoDocumento = dto.nombreTipoDocumento
            };

            _context.TiposDocumentos.Add(tipodocumento);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActualizarTiposDocumentos(ListaTiposDocumentoDto dto)
        {
            var tipoDocumento = await _context.TiposDocumentos
                .FirstOrDefaultAsync(t => t.tipoDocumentoId == dto.tipoDocumentoId);

            if (tipoDocumento == null)
                return false;

            tipoDocumento.nombreTipoDocumento = dto.nombreTipoDocumento;

            _context.TiposDocumentos.Update(tipoDocumento);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<ListaTiposDocumentoDto>> GetTiposDocumentosAsync(int tipoDocumentoId)
        {
            return await _context.TiposDocumentos
                .Where(td => td.tipoDocumentoId == tipoDocumentoId)
                .Select(td => new ListaTiposDocumentoDto
                {
                    tipoDocumentoId = td.tipoDocumentoId,
                    nombreTipoDocumento = td.nombreTipoDocumento
                })
                .ToListAsync();
        }


        //public async Task<bool> editartiposdocumentosasync(listatiposdocumentosdto dto, int id)
        //{
        //    var tipodocumento = new models.tipodocumento
        //    {
        //        nombretipodocumento = dto.nombretipodocumento
        //    };

        //    _context.tiposdocumentos.update(tipodocumento);
        //    await _context.savechangesasync();

        //    return true;
        //}
    }
}
