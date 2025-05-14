using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class RespuestaPostulante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int respuestaPostulanteId { get; set; }

        [ForeignKey("Postulante")]
        public int postulanteId { get; set; }
        public virtual Postulante Postulante { get; set; }

        [ForeignKey("PreguntaFiltro")]
        public int preguntaFiltroId { get; set; }
        public virtual PreguntaFiltro PreguntaFiltro { get; set; }

        [Required]
        public string valorRespuesta { get; set; } 

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; } 
        public virtual Usuario UsuarioUltMod { get; set; }
    }
}