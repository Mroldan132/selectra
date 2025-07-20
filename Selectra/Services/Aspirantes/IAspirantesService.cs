using Selectra.DTOs;

namespace Selectra.Services.Aspirantes
{
    public interface IAspirantesService
    {
        public Task<IEnumerable<ListaAspirantesDto>> GetListaAspirantesAsync();
        public Task<DetalleAspiranteDto> GetDetalleAspiranteAsync(int aspiranteId);
    }
}
