using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class LoginDto
    {
        [Required]
        public string CodUsuario { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
