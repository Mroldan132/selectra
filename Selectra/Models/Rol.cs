using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rolId { get; set; }
        [Required]
        [StringLength(100)]
        public string nombreRol { get; set; }
        public int? nivel { get; set; }
        [StringLength(500)]
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }
    }
}
