namespace Selectra.Services.Postulantes
{
    public interface IPostulanteService
    {
        Task<bool> PostularOfertaLaboral(int ofertaLaboralId, int aspiranteId);

    }
}
