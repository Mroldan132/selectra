namespace Selectra.DTOs
{
    public class RequerimientosAprobadosDto
    {
        public int requerimientoId { get; set; }
        public string nombreRequerimiento { get; set; }
        public string solicitante { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public DateTime? fechaAprobacion { get; set; }
        public string aprobador { get; set; }
    }
}
