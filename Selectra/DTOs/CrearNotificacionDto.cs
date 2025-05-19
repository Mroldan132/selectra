namespace Selectra.DTOs
{
    public class CrearNotificacionDto
    {
        public int usuarioId { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string mensaje { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        public DateTime fechaCreacion { get; set; } = DateTime.Now;
        public bool leida { get; set; } = false;
        public string link { get; set; } = string.Empty;

    }
}
