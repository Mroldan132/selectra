using Selectra.DTOs;

namespace Selectra.Services.OfertasLaborales
{
    public interface IOfertasLaboralesServices
    {
        Task<List<RequerimientosAprobadosDto>> GetRequerimientosAprobadosAsync();

        Task<DetalleOfertaLaboralDto> CrearOfertaLaboralRequerimiento(int requerimientoId);
        Task<bool> GenerarOfertaLaborarAsync(DetalleOfertaLaboralDto ofertaLaboralDto,int usuarioUltimaMod);
        Task<IEnumerable<ListaOfertasLaboralesDto>> GetListOfertasLaboralesAsync();
        Task<DetalleOfertaLaboralDto> DetalleOfertaLaboralRequerimientoAsync(int ofertaLaboralId);
    }
}
