namespace Selectra.DTOs
{
    public class DetalleOfertaLaboralPublicadasDto
    {
        public int ofertaId {get;set; }
        public string titulo {get;set; }
        public string publicadoPor {get;set; }
        public string fechaPublicacion {get;set;}
        public string ubicacion {get;set;}
        public string sueldo {get;set;}
        public string descripcionCompleta {get;set;}
        public string[] funciones {get;set; }
        public string[] beneficios{get;set;}
        public string[] competencias {get;set;}
    }
}
