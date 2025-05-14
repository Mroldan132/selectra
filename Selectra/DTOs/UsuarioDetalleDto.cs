namespace Selectra.DTOs
{
    public class UsuarioDetalleDto
    {
        public int UsuarioId { get; set; }
        public string CodUsuario { get; set; }
        public bool Activo { get; set; }
        public string NombreRol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltMod { get; set; }
    }
}
