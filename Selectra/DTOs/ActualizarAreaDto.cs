using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ActualizarAreaDto
    {
        [Required(ErrorMessage = "El ID del area es obligatorio.")]
        public int AreaId { get; set; }
        [Required(ErrorMessage = "El nombre de area es obligatorio.")]
        public string NombreArea { get; set; }
        [Required(ErrorMessage = "La descripcon del area es obligatoria.")]
        public string Descripcion { get; set; }
    }
}