using Selectra.DTOs;

namespace Selectra.Services.Postulantes
{
    public interface IPostulanteService
    {
        Task<bool> PostularOfertaLaboral(int ofertaLaboralId, int aspiranteId);

        Task<List<MisOfertasLaboralesDto>> ListaMisOfertasLaborales(int usuarioId);

    }
}
