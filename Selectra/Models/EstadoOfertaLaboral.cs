using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class EstadoOfertaLaboral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int estadoOfertaLaboralId { get; set; }
        [Required]
        [StringLength(3)]
        public string codigoEstado { get; set; }
        [Required]
        [StringLength(100)]
        public string nombreEstado { get; set; }
        public bool esPublica { get; set; }
        public bool esEditable { get; set; }
    }
}
