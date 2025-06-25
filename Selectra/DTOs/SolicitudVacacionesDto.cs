namespace Selectra.DTOs
{
    public class SolicitudVacacionesDto
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double DiasSolicitados { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        public string ComentariosEmpleado { get; set; }
        public string ComentariosAprobador { get; set; }
    }
}
