using Selectra.DTOs;

namespace Selectra.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    }
}
