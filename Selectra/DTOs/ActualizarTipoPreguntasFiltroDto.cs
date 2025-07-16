using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ActualizarTipoPreguntasFiltroDto
    {
        [Required(ErrorMessage = "El ID del tipo de pregunta es obligatorio.")]
        public int tipoPreguntaId { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de pregunta es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string nombre { get; set; }
    }
}
