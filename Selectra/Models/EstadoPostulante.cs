using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class EstadoPostulante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int estadoPostulanteId { get; set; }
        [Required]
        [StringLength(3)]
        public string codigoEstado { get; set; }
        [Required]
        [StringLength(100)]
        public string nombreEstado { get; set; }
        public bool esEstadoContratacion { get; set; }
        public bool esEstadoRechazo { get; set; }
    }
}
