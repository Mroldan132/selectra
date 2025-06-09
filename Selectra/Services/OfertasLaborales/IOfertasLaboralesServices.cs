using Selectra.DTOs;

namespace Selectra.Services.OfertasLaborales
{
    public interface IOfertasLaboralesServices
    {
        Task<List<RequerimientosAprobadosDto>> GetRequerimientosAprobadosAsync();


    }
}
