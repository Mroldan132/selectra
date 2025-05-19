using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Selectra.DTOs;
using Selectra.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Selectra.Services
{
    public class AuthService : IAuthService
    {
        private SelectraContext _context;
        private IConfiguration _configuration;

        public AuthService(SelectraContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios
                                    .Include(u => u.Rol)
                                    .SingleOrDefaultAsync(u => u.codUsuario == loginDto.CodUsuario && u.activo);
            if (usuario == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Clave, usuario.claveHash)) 
            {
                return null;
            }

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.usuarioId.ToString()), 
                    new Claim(ClaimTypes.Name, usuario.codUsuario),
                    new Claim(ClaimTypes.Role, usuario.Rol?.nombreRol ?? string.Empty) 
                                                                                       
                }),
                Expires = DateTime.UtcNow.AddMinutes(1), 
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResponseDto
            {
                Token = tokenString,
                Expiration = tokenDescriptor.Expires.Value,
                Usuario = usuario.codUsuario,
                usuarioId = usuario.usuarioId,
                Rol = usuario.Rol?.nombreRol ?? "Sin Rol Asignado"
            };
        }
    }
}
