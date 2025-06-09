using Selectra.DTOs;

namespace Selectra.Services.NivelesAcademicos
{
    public interface INivelesAcademicosService
    {
        public Task<List<ListaNivelesAcademicosDto>> GetListaNivelesAcademicosAsync();
    }
}
