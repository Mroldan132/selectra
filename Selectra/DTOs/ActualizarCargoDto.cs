using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ActualizarCargoDto
    {
        [Required(ErrorMessage = "El ID del cargo es obligatorio.")]
        public int CargoId { get; set; }

        [Required(ErrorMessage = "El nombre del cargo es obligatorio.")]
        public string NombreCargo { get; set; }

        [Required(ErrorMessage = "La descripción del cargo es obligatoria.")]
        public string Descripcion { get; set; }
    }
}
