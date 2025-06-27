using Selectra.DTOs;

namespace Selectra.Services.TipoDocumento
{
    public interface ITiposDocumentosService
    {
        public Task<IEnumerable<ListaTiposDocumentoDto>> GetListaTiposDocumentosAsync();

        public Task<bool> CrearTiposDocumentosAsync(ListaTiposDocumentoDto dto);
        //public Task<IEnumerable<ListaTiposDocumentoDto>> CrearTiposDocumentosAsync(ListaTiposDocumentoDto dto);

        ////public Task<bool> EditarTiposDocumentosAsync(ListaTiposDocumentoDto dto, int id);
        //public Task<IEnumerable<ListaTiposDocumentoDto>> EditarTiposDocumentosAsync(ListaTiposDocumentoDto dto, int id);

        ////public Task<ListaTiposDocumentoDto?> GetTiposDocumentosByIdAsync(int id);
        public Task<IEnumerable<ListaTiposDocumentoDto>> GetTiposDocumentosAsync(int tipoDocumentoId);

        public Task<bool> ActualizarTiposDocumentos(ListaTiposDocumentoDto dto);
        //public Task<IEnumerable<ListaTiposDocumentoDto>> ActualizarTiposDocumentos(ListaTiposDocumentoDto dto);

    }
}
