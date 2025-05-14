using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class OrdenAprobacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ordenAprobacionId { get; set; }

        [ForeignKey("TipoRequerimiento")]
        public int tipoRequerimientoId { get; set; }
        public virtual TiposRequerimiento TipoRequerimiento { get; set; }

        [Required]
        public int orden { get; set; } 

        [ForeignKey("RolAprobador")]
        public int rolAprobadorId { get; set; }
        public virtual Rol RolAprobador { get; set; }

        [StringLength(200)]
        public string descripcionPaso { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; }
        public virtual Usuario UsuarioUltMod { get; set; }

        public virtual ICollection<HistorialAprobacion> HistorialAprobaciones { get; set; } = new HashSet<HistorialAprobacion>();
    }
}