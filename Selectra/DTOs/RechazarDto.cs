using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class RechazarDto
    {
        [Required]
        public int AprobadorId { get; set; }

        [Required(ErrorMessage = "El motivo de rechazo es obligatorio.")]
        public string Motivo { get; set; }
    }
}
