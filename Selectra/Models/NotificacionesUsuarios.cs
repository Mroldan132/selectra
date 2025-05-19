using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class NotificacionesUsuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int notificacionId { get; set; }
        [ForeignKey("Usuario")]
        public int usuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        [Required]
        [StringLength(100)]
        public string titulo { get; set; }
        [Required]
        [StringLength(500)]
        public string mensaje { get; set; }
        [Required]
        public string tipo { get; set; }
        [Required]
        public DateTime fechaCreacion { get; set; }
        [Required]
        public bool leida { get; set; }
        [Required]
        public string link { get; set; }
        public bool estado { get; set; } = true;


    }
}
