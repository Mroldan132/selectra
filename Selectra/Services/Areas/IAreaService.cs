using Selectra.DTOs;

namespace Selectra.Services.Areas
{
    public interface IAreaService
    {
        Task<IEnumerable<ListaAreasDto>> GetListaAreasAsync();
        Task<bool> GenerarAreaAsync(DetalleAreaDto areaDto);
        Task<bool> ActualizarAreaAsync(int idArea, ActualizarAreaDto areaDto);
    }
}
