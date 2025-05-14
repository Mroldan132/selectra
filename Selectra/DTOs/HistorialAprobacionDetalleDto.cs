namespace Selectra.DTOs
{
    public class HistorialAprobacionDetalleDto
    {
        public int HistorialAprobacionId { get; set; }
        public string NombrePaso { get; set; } 
        public int Orden { get; set; }
        public string NombreAprobador { get; set; }
        public string EstadoDecision { get; set; } 
        public string Observaciones { get; set; }
        public DateTime? FechaDecision { get; set; }
        public DateTime FechaCreacionPaso { get; set; } 
    }
}
