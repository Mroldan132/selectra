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
        public int? jefeDirectoId { get; set; } 

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

        public virtual ICollection<Personal> Subordinados { get; set; } = new HashSet<Personal>();
        public virtual ICollection<RequerimientoPersonal> RequerimientosSolicitados { get; set; } = new HashSet<RequerimientoPersonal>();
        public virtual ICollection<HistorialAprobacion> AprobacionesRealizadas { get; set; } = new HashSet<HistorialAprobacion>();
        public virtual ICollection<OfertaLaboral> OfertasResponsable { get; set; } = new HashSet<OfertaLaboral>();
        [InverseProperty("JefeDestino")]
        public virtual ICollection<RequerimientoPersonal> RequerimientosComoJefeDestino { get; set; } = new HashSet<RequerimientoPersonal>();

    }
}