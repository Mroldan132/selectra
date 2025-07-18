using Selectra.DTOs;

namespace Selectra.Services.TipoPreguntasFiltro
{
    public interface ITipoPreguntasFiltroService
    {
        Task<IEnumerable<ListaTipoPreguntasFiltroDto>> GetListaTipoPreguntasFiltroAsync();
        Task<ListaTipoPreguntasFiltroDto?> GetTipoPreguntasFiltroPorIdAsync(int id);
        Task<bool> CrearTipoPreguntasFiltroAsync(CrearTipoPreguntasFiltroDto dto);
        Task<bool> ActualizarTipoPreguntasFiltroAsync(ActualizarTipoPreguntasFiltroDto dto);
        Task<bool> EliminarTipoPreguntasFiltroAsync(int id); 
    }

}
