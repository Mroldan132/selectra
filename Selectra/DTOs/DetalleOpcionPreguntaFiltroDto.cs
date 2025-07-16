
namespace Selectra.DTOs
{
    
    public class DetalleOpcionPreguntaFiltroDto
    {
        
        public int preguntaFiltroId { get; set; }
        public string textoOpcion { get; set; }
        public int? orden { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltMod { get; set; }
        public int? usuarioUltModId { get; set; }
    }
}
