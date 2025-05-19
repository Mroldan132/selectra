namespace Selectra.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public int usuarioId { get; set; }
        public DateTime Expiration { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
    }
}
