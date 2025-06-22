using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectra.Models
{
    public class Aspirantes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int aspiranteId { get; set; }

        [ForeignKey("DatosPersonales")]
        public int datosPersonalesId { get; set; }
        public virtual DatosPersonales DatosPersonales { get; set; }
        [ForeignKey("NivelAcademico")]
        public int nivelAcademicoId { get; set; }
        public virtual NivelAcademicos NivelAcademico { get; set; }
        [Required]
        public bool estado { get; set; }
        public string? pathCV { get; set; }
        public string? pathFoto { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }
    }
}
