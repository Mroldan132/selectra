using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selectra.DTOs;
using Selectra.Services;

namespace Selectra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResponse = await _authService.LoginAsync(loginDto);

            if (loginResponse == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas o usuario inactivo." });
            }

            return Ok(loginResponse);
        }
    }
}
