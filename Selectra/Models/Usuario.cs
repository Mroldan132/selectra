using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int usuarioId { get; set; }
        [Required]
        [StringLength(12)]
        public string codUsuario { get; set; }
        [Required]
        public string claveHash { get; set; }
        [ForeignKey("Rol")]
        public int rolId { get; set; }
        public virtual Rol Rol { get; set; }
        public bool activo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }
      
    }
}
