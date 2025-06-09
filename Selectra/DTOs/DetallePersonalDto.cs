namespace Selectra.DTOs
{
    public class DetallePersonalDto
    {
        public int personalId { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int tipoDocumentoId { get; set; }
        public string numeroDocumento { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string ubigeoNacimiento { get; set; }
        public string ubigeoResidencia { get; set; }
        public string telefono { get; set; }
        public string emailPersonal { get; set; }
        public int areaId { get; set; }
        public int cargoId { get; set; }
        public int? jefeDirectoId { get; set; }
        public string emailCorporativo { get; set; }
        public DateTime? fechaIngresoCompania { get; set; }
        public bool activo { get; set; }
        public int rolId { get; set; }
        public string codUsuario { get; set; }
    }
}
