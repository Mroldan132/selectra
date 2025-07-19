namespace Selectra.DTOs
{
    public class ListaOpcionPreguntaFiltroDto
    {
        public int opcionPreguntaId { get; set; }
        public int preguntaFiltroId { get; set; }
        public string textoOpcion { get; set; }
        public int? orden { get; set; }
    }
}