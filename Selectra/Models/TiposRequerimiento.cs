using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class TiposRequerimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tipoRequerimientoId { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; }
        public virtual Usuario UsuarioUltMod { get; set; }

        public virtual ICollection<RequerimientoPersonal> RequerimientosPersonales { get; set; } = new HashSet<RequerimientoPersonal>();
        public virtual ICollection<OrdenAprobacion> OrdenesAprobacion { get; set; } = new HashSet<OrdenAprobacion>();
    }
}