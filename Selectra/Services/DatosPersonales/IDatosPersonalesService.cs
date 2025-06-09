using Selectra.DTOs;

namespace Selectra.Services.DatosPersonales
{
    public interface IDatosPersonalesService
    {
        Task<List<ListaTiposDocumentoDto>> GetListaTiposDocumentoAsync();
        Task<List<ListaUbigeoDto>> GetListaDepartamentosAsync();
        Task<List<ListaUbigeoDto>> GetListaProvinciasAsync(string departamentoId);
        Task<List<ListaUbigeoDto>> GetListaDistritosAsync(string provinciaId);
    }
}
