using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class SolicitudVacaciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int personalId { get; set; }
        public virtual Personal Personal { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int estadoId { get; set; }
        public virtual EstadoSolicitudVacaciones Estado { get; set; }

        public string ComentariosEmpleado { get; set; }
        public string ComentariosAprobador { get; set; }

        public int? AprobadorId { get; set; }
        public virtual Personal Aprobador { get; set; }
    }
}
