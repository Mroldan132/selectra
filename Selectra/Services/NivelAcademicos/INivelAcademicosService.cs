using Selectra.DTOs;

namespace Selectra.Services.NivelAcademicos
{
    public interface INivelAcademicosService
    {
      
        public Task<List<ListaNivelAcademicosDto>> GetListaNivelAcademicosAsync();
        public Task<bool> CrearNivelAcademicosAsync(ListaNivelAcademicosDto dto);
        public Task<bool> ActualizarNivelAcademicosAsync(ActualizarNivelAcademicosDto nivelAcademicosDto, int nombreId);
    }
}
