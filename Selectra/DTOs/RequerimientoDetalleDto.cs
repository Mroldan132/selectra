namespace Selectra.DTOs
{
    public class RequerimientoDetalleDto
    {
        public int RequerimientoId { get; set; }
        public string TituloRequerimiento { get; set; }
        public string TipoRequerimientoNombre { get; set; }
        public string AreaNombre { get; set; }
        public string CargoNombre { get; set; }
        public string SolicitanteNombre { get; set; }
        public string Motivo { get; set; }
        public decimal? SueldoPropuesto { get; set; }
        public DateTime? FechaDeseadaIngreso { get; set; }
        public string JefeDestinoNombre { get; set; } 
        public string EstadoActualNombre { get; set; }
        public DateTime FechaCreacionRequerimiento { get; set; }
        public DateTime? FechaFinProceso { get; set; }
        public string UsuarioUltModNombre { get; set; } 
        public DateTime FechaUltModRequerimiento { get; set; }

        public List<HistorialAprobacionDetalleDto> HistorialDeAprobaciones { get; set; } = new List<HistorialAprobacionDetalleDto>();
    }
}
