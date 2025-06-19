namespace Selectra.DTOs
{
    public class DetalleAreaDto
    {
        public int areaId { get; set; }
        public string nombreArea { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaUltMod { get; set; }

    }
}