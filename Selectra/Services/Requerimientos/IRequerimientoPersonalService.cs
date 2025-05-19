using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Requerimiento
{
    public interface IRequerimientoPersonalService
    {
        Task<RequerimientoCreadoDto> CrearRequerimientoAsync(CrearRequerimientoDto dto, int solicitanteId, int usuarioQueCreaId);
        Task<RequerimientoCreadoDto> ActualizarRequerimientoAsync(int idRequerimiento,ActualizarRequerimientoDto dto, int usuarioQueCreaId);
        Task<IEnumerable<MisRequerimientosListDto>> GetMisRequerimientosAsync(int solicitantePersonalId);
        Task<RequerimientoDetalleDto> GetRequerimientoDetalleAsync(int requerimientoId, int usuarioActualPersonalId);
        Task<IEnumerable<ListaTipoRequerimientosDto>> GetListaTiposRequerimientosAsync();
        Task<string> AprobarRechazarRequerimientoAsync(AprobarRechazarRequerimientoDto aprobarRechazardto,int usuarioUltMod);
        Task<IEnumerable<ListaEstadosRequerimientoDto>> GetListaEstadosRequerimientoAsync();

    }
}
