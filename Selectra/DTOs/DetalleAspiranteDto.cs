namespace Selectra.DTOs
{
    public class DetalleAspiranteDto
    {
        public string codUsuario { get; set; }
        public int aspiranteId { get; set; }

        public bool estado { get; set; }

        public string? pathCV { get; set; }

        public string? pathFoto { get; set; }

        public string? DatosPersonales { get; set; }

        public String? NivelAcademico { get; set; }

        public DateTime fechaCreacion { get; set; }

        public DateTime fechaUltMod { get; set; }

        public int rolId { get; set; }

        public string nombres { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public int tipoDocumentoId { get; set; }

        public string numeroDocumento { get; set; }

        public DateTime fechaNacimiento { get; set; }

        public string ubigeoNacimiento { get; set; }

        public string ubigeoResidencia { get; set; }

        public string telefono { get; set; }

        public string emailPersonal { get; set; }
    }
}
