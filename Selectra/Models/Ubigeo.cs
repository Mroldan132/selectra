using System.ComponentModel.DataAnnotations;

namespace Selectra.Models
{
    public class Ubigeo
    {
        [Key]
        [StringLength(6)] 
        public string ubigeoId { get; set; } 

        [StringLength(2)]
        public string departamentoId { get; set; }

        [StringLength(100)]
        public string departamento { get; set; }
        [StringLength(4)]
        public string distritoId { get; set; }
        [StringLength(100)]
        public string distrito { get; set; }
        [StringLength(100)]
        public string provincia { get; set; }



    }
}
