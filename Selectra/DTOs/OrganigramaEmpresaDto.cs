namespace Selectra.DTOs
{
    public class OrganigramaEmpresaDto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string cargo{ get; set; }
        public int? jefeDirectoId { get; set; }
    }
}
