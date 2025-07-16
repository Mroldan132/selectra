using Selectra.DTOs;

namespace Selectra.Services.PreguntasFiltros
{
    public interface IPreguntasFiltrosService
    {
        public Task<IEnumerable<ListaPreguntasFiltrosDto>> GetListaPreguntasFiltrosAsync();
        public Task<bool> GenerarPreguntaFiltroAsync(DetallePreguntasFiltrosDto preguntaFiltroDto);
        public Task<bool> ActualizarPreguntaFiltroAsync(int idPreguntaFiltro, ActualizarPreguntasFiltrosDto preguntaFiltroDto);

    }
}
