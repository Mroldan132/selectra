namespace Selectra.DTOs
{
    public class ListaRequerimientosAprobadosDto
    {
        public int IdRequerimiento { get; set; }
        public string NombreRequerimiento { get; set; } 
        public string TipoRequerimiento { get; set; }
        public string EstadoRequerimiento { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Solicitante { get; set; }


    }
}
