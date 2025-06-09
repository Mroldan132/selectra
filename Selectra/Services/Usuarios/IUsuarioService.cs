using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Usuarios
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarPersonalAsync(RegistrarPersonalDto registroDto, int usuarioQueRegistraId);
        Task<Usuario> RegistrarAdministradorAsync(RegistrarAdministradorDto registroDto, int usuarioQueRegistraId);
        Task<Usuario> RegistrarAspiranteAsync(RegistrarAspiranteDto registroDto, int usuarioQueRegistraId);
        Task<UsuarioDetalleDto> GetUsuarioPorIdAsync(int id);
        Task<List<ListaRolesDto>> GetListaRolesAync();

    }
}
