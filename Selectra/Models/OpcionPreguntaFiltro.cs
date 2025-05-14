using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class OpcionPreguntaFiltro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int opcionPreguntaId { get; set; }

        [ForeignKey("PreguntaFiltro")]
        public int preguntaFiltroId { get; set; }
        public virtual PreguntaFiltro PreguntaFiltro { get; set; }

        [Required]
        [StringLength(500)]
        public string textoOpcion { get; set; }

        public int? orden { get; set; }

        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }

        [ForeignKey("UsuarioUltMod")]
        public int? usuarioUltModId { get; set; }
        public virtual Usuario UsuarioUltMod { get; set; }
    }
}