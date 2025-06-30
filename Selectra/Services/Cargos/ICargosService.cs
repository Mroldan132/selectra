using Selectra.DTOs;

namespace Selectra.Services.Cargos
{
    public interface ICargosService
    {
        Task<IEnumerable<ListaCargosDto>> GetListaCargosAsync();
        Task<bool> GenerarCargoAsync(DetalleCargoDto cargoDto);
        Task<bool> ActualizarCargoAsync(int idCargo, ActualizarCargoDto cargoDto);
    }
}
