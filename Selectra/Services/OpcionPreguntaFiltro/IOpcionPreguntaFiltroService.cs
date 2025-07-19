using Selectra.DTOs;

namespace Selectra.Services.OpcionPreguntaFiltro
{
    public interface IOpcionPreguntaFiltroService
    {
        Task<IEnumerable<ListaOpcionPreguntaFiltroDto>> GetListaOpcionPreguntaFiltroAsync();
        Task<bool> GenerarOpcionPreguntaFiltroAsync(DetalleOpcionPreguntaFiltroDto opcionPreguntaFiltroDto);

        Task<bool> ActualizarOpcionPreguntaFiltroAsync(int idOpcionPreguntaFiltro, ActualizarOpcionPreguntaFiltroDto actualizarOpcionPreguntaFiltroDto);
        Task<bool> EliminarOpcionPreguntaFiltroAsync(int idOpcionPreguntaFiltro);
    }
}
