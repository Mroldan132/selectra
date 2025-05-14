using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int personalId { get; set; }

        [Required]
        public int datosPersonalesId { get; set; }
        [ForeignKey("datosPersonalesId")]
        public virtual DatosPersonales DatosPersonales { get; set; }

        [Required] 
        public int usuarioId { get; set; }
        [ForeignKey("usuarioId")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("JefeDirecto")]
        public int? jefeDirectoId { get; set; } 
        public virtual Personal JefeDirecto { get; set; }

        [ForeignKey("Area")]
        public int areaId { get; set; }
        public virtual Area Area { get; set; }

        [ForeignKey("Cargo")]
        public int cargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Required]
        [StringLength(100)]
        public string emailCorporativo { get; set; }

        public DateTime? fechaIngresoCompania { get; set; } 

        public bool activo { get; set; } = true; 

        // --- Relaciones Inversas (Colecciones) ---
        // Para saber a quién supervisa directamente
        public virtual ICollection<Personal> Subordinados { get; set; } = new HashSet<Personal>();
        // Para saber qué requerimientos solicitó
        public virtual ICollection<RequerimientoPersonal> RequerimientosSolicitados { get; set; } = new HashSet<RequerimientoPersonal>();
        // Para saber qué aprobaciones realizó
        public virtual ICollection<HistorialAprobacion> AprobacionesRealizadas { get; set; } = new HashSet<HistorialAprobacion>();
        // Para saber de qué ofertas es responsable (RRHH)
        public virtual ICollection<OfertaLaboral> OfertasResponsable { get; set; } = new HashSet<OfertaLaboral>();
        // Para saber qué requerimientos tiene como jefe destino
        [InverseProperty("JefeDestino")]
        public virtual ICollection<RequerimientoPersonal> RequerimientosComoJefeDestino { get; set; } = new HashSet<RequerimientoPersonal>();

    }
}