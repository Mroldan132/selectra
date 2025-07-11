namespace Selectra.DTOs
{
    public class DetalleMisOfertasLaborales
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public MisOfertasLaboralesDto oferta { get; set; }

    }
}
