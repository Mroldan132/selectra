namespace Selectra.DTOs
{
    public class ListaPreguntasFiltrosDto
    {
        public int preguntaFiltroId { get; set; }
        public string textoPregunta { get; set; }
        public int tipoPreguntaId { get; set; }
        public string nombreTipoPregunta { get; set; } // NUEVO
        public bool obligatoria { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
