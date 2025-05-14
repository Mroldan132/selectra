using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Para ICollection

namespace Selectra.Models
{
    public class RequerimientoPersonal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int requerimientoId { get; set; }

        [ForeignKey("TipoRequerimiento")]
        public int tipoRequerimientoId { get; set; }
        public virtual TiposRequerimiento TipoRequerimiento { get; set; }

        [Required]
        [StringLength(250)]
        public string tituloRequerimiento { get; set; }

        [ForeignKey("Solicitante")]
        public int solicitanteId { get; set; }
        public virtual Personal Solicitante { get; set; }

        [ForeignKey("AreaDestino")]
        public int areaId { get; set; }
        public virtual Area AreaDestino { get; set; } 

        [ForeignKey("CargoSolicitado")]
        public int cargoId { get; set; }
        public virtual Cargo CargoSolicitado { get; set; } 

        [Required]
        public string motivo { get; set; } 

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal? sueldoPropuesto { get; set; } 

        public DateTime? fechaDeseadaIngreso { get; set; } 

        [ForeignKey("JefeDestino")]
        public int? jefeDestinoId { get; set; } 
        public virtual Personal JefeDestino { get; set; }

        [ForeignKey("EstadoRequerimiento")]
        public int estadoRequerimientoId { get; set; }
        public virtual EstadoRequerimiento EstadoRequerimiento { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; }
        public virtual Usuario UsuarioUltMod { get; set; }

        public DateTime? fechaFinProceso { get; set; } 

        public virtual ICollection<HistorialAprobacion> HistorialAprobaciones { get; set; } = new HashSet<HistorialAprobacion>();
        public virtual ICollection<OfertaLaboral> OfertasLaborales { get; set; } = new HashSet<OfertaLaboral>(); 
    }
}