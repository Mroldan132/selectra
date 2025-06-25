using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class EstadoSolicitudVacaciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; } // 1: Pendiente, 2: Aprobada, 3: Rechazada, 4: Cancelada
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
