
using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ActualizarOpcionPreguntaFiltroDto
    {
        [Required(ErrorMessage = "El texto de la opción es obligatorio.")]
        [StringLength(100, ErrorMessage = "El texto de la opción no puede exceder los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "El texto de la opción solo puede contener letras, números y espacios.")]
        public string textoOpcion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El orden debe ser un número entero no negativo.")]

        public int orden { get; set; }
    }
}
