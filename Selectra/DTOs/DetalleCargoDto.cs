namespace Selectra.DTOs
{
    public class DetalleCargoDto
    {
        public int cargoId { get; set; }
        public string nombreCargo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaUltMod { get; set; }
    }
}
