using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Usuarios
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarUsuarioAsync(RegistrarUsuarioDto registroDto, int usuarioQueRegistraId);
        Task<UsuarioDetalleDto> GetUsuarioPorIdAsync(int id);
    }
}
