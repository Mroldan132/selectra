

namespace Selectra.DTOs
{
    public class DetallePreguntasFiltrosDto 
    {
        public int preguntaFiltroId { get; set; }
        public int tipoPreguntaId { get; set; }
        public string textoPregunta { get; set; }
        public bool obligatoria { get; set; } = true;
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }

    }
}
