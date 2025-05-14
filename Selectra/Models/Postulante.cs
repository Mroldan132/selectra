using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class Postulante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int postulanteId { get; set; }

        [ForeignKey("OfertaLaboral")]
        public int ofertaId { get; set; }
        public virtual OfertaLaboral OfertaLaboral { get; set; }

        [Required]
        [StringLength(300)]
        public string nombreCompleto { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(50)]
        public string telefono { get; set; }

        [StringLength(50)]
        public string tipoDocumento { get; set; }

        [StringLength(20)]
        public string numeroDocumento { get; set; } 

        [StringLength(500)] 
        public string cvPath { get; set; }

        [Required]
        public DateTime fechaPostulacion { get; set; }

        [ForeignKey("EstadoPostulante")]
        public int estadoPostulanteId { get; set; }
        public virtual EstadoPostulante EstadoPostulante { get; set; }

        [StringLength(100)]
        public string fuenteReclutamiento { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; } 
        public virtual Usuario UsuarioUltMod { get; set; }

        public virtual ICollection<RespuestaPostulante> RespuestasPostulante { get; set; } = new HashSet<RespuestaPostulante>();

    }
}