using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class RegistrarAspiranteDto : IDatosPersonalesBasicosDto
    {

        [Required]
        [StringLength(50)]
        public string CodUsuario { get; set; }

        [Required]
        [MinLength(8)]
        public string Clave { get; set; }

        public bool Activo { get; set; } = true;

        [Required]
        [StringLength(200)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [StringLength(200)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombres { get; set; }

        [Required]
        public int TipoDocumentoId { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [StringLength(40)]
        public string Telefono { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string EmailPersonal { get; set; }

        [StringLength(6)]
        public string UbigeoNacimiento { get; set; }

        [StringLength(6)]
        public string UbigeoResidencia { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        [Required]
        public int NivelAcademicoId { get; set; }
        public string? PathCV { get; set; }
        public string? PathFoto { get; set; }
    }
}
