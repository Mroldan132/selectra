namespace Selectra.DTOs
{
    public class DetalleOfertaLaboralDto
    {
        public string titulo { get; set; }
        public string descripcion{ get; set; }
        public string funciones { get; set; }
        public string beneficios { get; set; }
        public string competencias { get; set; }
        public double sueldoOfrecido { get; set; }
        public int areaId { get; set; }
        public int cargoId { get; set; }
        public int responsable { get; set; }
        public string direccionTrabajo { get; set; }
        public string referenciaUbicacion { get; set; }
        public DateTime fechaCreacion { get;set; }
        public DateTime? fechaPublicacion { get; set; }
        public DateTime? fechaCierre { get; set; }
        public DateTime? fechaEstimadaIngreso { get; set; }

    }
}
