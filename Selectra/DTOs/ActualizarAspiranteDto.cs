using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ActualizarAspiranteDto : IDatosPersonalesBasicosDto
    {

        [Required]
        [StringLength(50)]
        public string CodUsuario { get; set; }

        [Required]
        [MinLength(8)]
        public string Clave { get; set; }

        public bool Activo { get; set; } = true;

        [Required]
        public int RolId { get; set; }
        // Implementación de la interfaz IDatosPersonalesBasicosDto
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
        public string? Telefono { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string? EmailPersonal { get; set; }

        [StringLength(6)]
        public string? UbigeoNacimiento { get; set; }

        [StringLength(6)]
        public string? UbigeoResidencia { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        // Datos específicos de la tabla Personal
        public string? pathCV { get; set; }
        public string? pathFoto { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }

        [Required]
        public bool estado { get; set; }

        [Required]
        public int nivelAcademicoId { get; set; }
    }
}
