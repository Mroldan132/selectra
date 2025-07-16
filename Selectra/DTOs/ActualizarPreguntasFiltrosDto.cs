
using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ActualizarPreguntasFiltrosDto
    {
        [Required(ErrorMessage = "El tipo de pregunta es obligatorio.")]
        public int tipoPreguntaId { get; set; }

        [Required(ErrorMessage = "El texto de la pregunta es obligatorio.")]
        [StringLength(500, ErrorMessage = "El texto de la pregunta no puede exceder los 500 caracteres.")]
        public string textoPregunta { get; set; }

        [Required(ErrorMessage = "El campo 'obligatoria' es obligatorio.")]
        public bool obligatoria { get; set; }
    }
}

}
