using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Personales
{
    public class PersonalesServices : IPersonalesServices
    {
        private readonly SelectraContext _context;

        public PersonalesServices(SelectraContext context) { 
            _context = context;
        }

        public async Task<IEnumerable<ListaJefesPersonalDto>> GetListaJefesDirectosAsync() =>
            await _context.Personales
            .Select(p => new ListaJefesPersonalDto
            {
                PersonalId = p.personalId,
                NombrePersonal =$"{p.DatosPersonales.apellidoPaterno} {p.DatosPersonales.apellidoMaterno} {p.DatosPersonales.nombres}",
                NombreCargo = p.Cargo.nombreCargo
            })
            .ToListAsync();
    }
}
