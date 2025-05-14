using Selectra.DTOs;

namespace Selectra.Services.Areas
{
    public interface IAreaService
    {
        Task<IEnumerable<ListaAreasDto>> GetListaAreasAsync();
    }
}
