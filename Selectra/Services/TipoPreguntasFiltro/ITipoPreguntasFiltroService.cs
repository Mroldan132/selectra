using Selectra.DTOs;

namespace Selectra.Services.TipoPreguntasFiltro
{
    public interface ITipoPreguntasFiltroService
    {
        Task<IEnumerable<ListaTipoPreguntasFiltroDto>> GetListaTipoPreguntasFiltroAsync();
        Task<bool> CrearTipoPreguntasFiltroAsync(CrearTipoPreguntasFiltroDto dto);
        Task<bool> ActualizarTipoPreguntasFiltroAsync(ActualizarTipoPreguntasFiltroDto dto);
    }

}
