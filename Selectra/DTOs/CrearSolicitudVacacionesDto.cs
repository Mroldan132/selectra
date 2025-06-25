using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class CrearSolicitudVacacionesDto
    {
        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public string ComentariosEmpleado { get; set; }
    }
}
