using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class CrearRequerimientoDto
    {
        [Required(ErrorMessage = "El tipo de requerimiento es obligatorio.")]
        public int TipoRequerimientoId { get; set; }

        [Required(ErrorMessage = "El título del requerimiento es obligatorio.")]
        [StringLength(250, ErrorMessage = "El título no puede exceder los 250 caracteres.")]
        public string TituloRequerimiento { get; set; }

        [Required(ErrorMessage = "El área destino es obligatoria.")]
        public int AreaId { get; set; }

        [Required(ErrorMessage = "El cargo solicitado es obligatorio.")]
        public int CargoId { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio.")]
        public string Motivo { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "El sueldo propuesto debe ser un valor positivo.")]
        public decimal? SueldoPropuesto { get; set; }

        public DateTime? FechaDeseadaIngreso { get; set; } 

        public int? JefeDestinoId { get; set; } 
    }
}
