namespace Selectra.DTOs
{
    public interface IDatosPersonalesBasicosDto
    {
        string ApellidoPaterno { get; }
        string ApellidoMaterno { get; }
        string Nombres { get; }
        int TipoDocumentoId { get; }
        string NumeroDocumento { get; }
        string Telefono { get; }
        string EmailPersonal { get; }
        string UbigeoNacimiento { get; }
        string UbigeoResidencia { get; }
        DateTime? FechaNacimiento { get; } 
    }
}
