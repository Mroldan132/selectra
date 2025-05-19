namespace Selectra.DTOs
{
    public class RequerimientoCreadoDto
    {
        public int RequerimientoId { get; set; }
        public string TituloRequerimiento { get; set; }
        public string EstadoNombre { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public int? AprobadorId { get; set; }
    }
}
