using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class PreguntasFiltros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int preguntaFiltroId { get; set; }

        [ForeignKey("TipoPreguntasFiltros")]
        public int tipoPreguntaId { get; set; }
        public virtual TipoPreguntasFiltro TipoPreguntasFiltros { get; set; }

        [Required]
        public string textoPregunta { get; set; }

        public bool obligatoria { get; set; } = true;

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; }
        public virtual Usuario UsuarioUltMod { get; set; }

        public virtual ICollection<OpcionPreguntaFiltro> OpcionesPregunta { get; set; } = new HashSet<OpcionPreguntaFiltro>();
        public virtual ICollection<RespuestaPostulante> RespuestasPostulantes { get; set; } = new HashSet<RespuestaPostulante>();
    }
}