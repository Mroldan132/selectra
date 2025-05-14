using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class EstadoHistorialAprobacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int estadoHistorialAprobacionId { get; set; }
        [Required]
        [StringLength(3)]
        public string codigoEstado { get; set; }
        [Required]
        [StringLength(100)]
        public string nombreEstado { get; set; }

    }
}
