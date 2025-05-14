
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class EstadoRequerimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int estadoRequerimientoId { get; set; }
        [Required]
        [StringLength(3)]
        public string codigoEstado { get; set; }
        [Required]
        [StringLength(100)]
        public string nombreEstado { get; set; }
        public bool esEstadoInicial { get; set; }
        public bool esEstadoFinal { get; set; }
    }
}