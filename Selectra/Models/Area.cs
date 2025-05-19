using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int areaId { get; set; }
        [Required]
        [StringLength(100)]
        public string nombreArea { get; set; }
        [StringLength(500)]
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }
        public virtual ICollection<Personal> PersonalEnArea { get; set; } = new HashSet<Personal>();
        public virtual ICollection<RequerimientoPersonal> RequerimientosEnArea { get; set; } = new HashSet<RequerimientoPersonal>();
        public virtual ICollection<OfertaLaboral> OfertasEnArea { get; set; } = new HashSet<OfertaLaboral>();
    }
}
