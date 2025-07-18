using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class CrearTipoPreguntasFiltroDto
    {
        [Required(ErrorMessage = "El nombre del tipo de pregunta es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string nombre { get; set; }
    }
}
