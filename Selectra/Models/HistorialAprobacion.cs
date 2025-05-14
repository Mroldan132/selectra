using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class HistorialAprobacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int historialAprobacionId { get; set; }

        [ForeignKey("RequerimientoPersonal")]
        public int requerimientoId { get; set; }
        public virtual RequerimientoPersonal RequerimientoPersonal { get; set; }

        [ForeignKey("OrdenAprobacion")]
        public int ordenAprobacionId { get; set; }
        public virtual OrdenAprobacion OrdenAprobacion { get; set; } 

        [ForeignKey("Aprobador")]
        public int aprobadorId { get; set; }
        public virtual Personal Aprobador { get; set; } 

        [ForeignKey("EstadoHistorialAprobacion")]
        public int estadoHistorialAprobacionId { get; set; }
        public virtual EstadoHistorialAprobacion EstadoHistorialAprobacion { get; set; } 

        public string observaciones { get; set; } 

        public DateTime? fechaDecision { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; }
        public virtual Usuario UsuarioUltMod { get; set; }
    }
}