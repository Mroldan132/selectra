using Selectra.DTOs;

namespace Selectra.Services.Personales
{
    public interface IPersonalesServices
    {
        public Task<IEnumerable<ListaJefesPersonalDto>> GetListaJefesDirectosAsync();
    }
}
