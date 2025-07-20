using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class RechazarDto
    {

        [Required(ErrorMessage = "El motivo de rechazo es obligatorio.")]
        public string Motivo { get; set; }
    }
}
