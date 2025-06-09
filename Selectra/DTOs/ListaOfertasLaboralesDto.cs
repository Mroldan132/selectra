namespace Selectra.DTOs
{
    public class ListaOfertasLaboralesDto
    {
        public int ofertaLaboralId { get; set; }
        public string titulo { get; set; }
        public string area { get; set; }
        public decimal? sueldo { get; set; }
        public string estadoOferta { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
