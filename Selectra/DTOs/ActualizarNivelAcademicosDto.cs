using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ActualizarNivelAcademicosDto
    {
        [Required(ErrorMessage = "El ID del nivel académico es obligatorio.")]
        public int nivelAcademicoId { get; set; }

        [Required(ErrorMessage = "El nombre del nivel académico es obligatorio.")]
        public string nombre { get; set; }

    }
}
