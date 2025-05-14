using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class DatosPersonales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int datosPersonalesId { get; set; }
        [Required]
        [StringLength(200)]
        public string apellidoPaterno { get; set; }
        [Required]
        [StringLength(200)]
        public string apellidoMaterno { get; set; }
        [Required]
        [StringLength(200)]
        public string nombres { get; set; }
        [ForeignKey("TipoDocumento")]
        public int tipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        [Required]
        [StringLength(20)]
        public string numeroDocumento { get; set; }
        [StringLength(40)]
        public string telefono { get; set; }
        [StringLength(50)]
        public string emailPersonal { get; set; }
        [StringLength(6)]
        public string ubigeoNacimiento { get; set; }
        [StringLength(6)]
        public string ubigeoResidencia { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }

    }
}
