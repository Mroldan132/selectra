using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class TipoPreguntasFiltro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tipoPreguntaId { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public virtual ICollection<PreguntaFiltro> PreguntasFiltro { get; set; } = new HashSet<PreguntaFiltro>();
    }
}