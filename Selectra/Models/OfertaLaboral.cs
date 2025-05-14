using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class OfertaLaboral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ofertaId { get; set; }

        [ForeignKey("RequerimientoPersonal")]
        public int? requerimientoId { get; set; }
        public virtual RequerimientoPersonal RequerimientoPersonal { get; set; }

        [Required]
        [StringLength(200)]
        public string titulo { get; set; }

        public string descripcion { get; set; } 
        public string funciones { get; set; }   
        public string beneficios { get; set; }  
        public string competencias { get; set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? sueldoOfrecido { get; set; } 

        [ForeignKey("Area")]
        public int areaId { get; set; }
        public virtual Area Area { get; set; }

        [ForeignKey("Cargo")]
        public int cargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [ForeignKey("Responsable")]
        public int responsableId { get; set; } 
        public virtual Personal Responsable { get; set; }

        [StringLength(300)]
        public string direccionTrabajo { get; set; }
        [StringLength(300)]
        public string referenciaUbicacion { get; set; }

        [ForeignKey("EstadoOfertaLaboral")]
        public int estadoOfertaLaboralId { get; set; }
        public virtual EstadoOfertaLaboral EstadoOfertaLaboral { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaPublicacion { get; set; } 
        public DateTime? fechaCierre { get; set; }      
        public DateTime? fechaEstimadaIngreso { get; set; } 
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; }
        public virtual Usuario UsuarioUltMod { get; set; }

        // --- Relaciones Inversas ---
        public virtual ICollection<PreguntaFiltro> PreguntasFiltro { get; set; } = new HashSet<PreguntaFiltro>();
        public virtual ICollection<Postulante> Postulantes { get; set; } = new HashSet<Postulante>();
    }
}