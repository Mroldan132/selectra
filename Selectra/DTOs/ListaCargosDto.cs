using System.ComponentModel.DataAnnotations;

namespace Selectra.DTOs
{
    public class ListaCargosDto
    {
        public int CargoId { get; set; }
        public string NombreCargo { get; set; }
        public string Descripcion { get; set; }
    }
}
