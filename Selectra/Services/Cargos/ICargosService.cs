using Selectra.DTOs;

namespace Selectra.Services.Cargos
{
    public interface ICargosService
    {
        public Task<IEnumerable<ListaCargosDto>> GetListaCargosAsync();
    }
}
