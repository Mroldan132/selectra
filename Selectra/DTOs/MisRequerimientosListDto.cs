using Selectra.Models;

namespace Selectra.DTOs
{
    public class MisRequerimientosListDto
    {
        public int RequerimientoId { get; set; }
        public string TituloRequerimiento { get; set; }
        public string TipoRequerimientoNombre { get; set; }
        public string CargoNombre { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public string EstadoNombre { get; set; }
        public List<HistorialAprobacionDto> HistorialDeAprobaciones { get; set; } = new List<HistorialAprobacionDto>();

    }
}
