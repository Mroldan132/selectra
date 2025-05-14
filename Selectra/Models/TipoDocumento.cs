using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class TipoDocumento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tipoDocumentoId { get; set; }
        [Required]
        [StringLength(100)]
        public string nombreTipoDocumento { get; set; }
    }
}
