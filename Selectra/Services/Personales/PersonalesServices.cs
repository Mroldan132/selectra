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

        public async Task<IEnumerable<ListaPersonalDto>> GetListaPersonalessAsync()
        {
            return await _context.Personales
                .Include(p => p.DatosPersonales)
                .Include(p => p.Cargo)
                .Include(p => p.Area)
                .Select(p => new ListaPersonalDto { 
                    personalId = p.personalId,
                    codUsuario = p.DatosPersonales.Usuario.codUsuario,
                    nombres = p.DatosPersonales.nombres,
                    apellidoPaterno = p.DatosPersonales.apellidoPaterno,
                    apellidoMaterno = p.DatosPersonales.apellidoMaterno,
                    rolNombre = p.DatosPersonales.Usuario.Rol.nombreRol,
                    areaNombre = p.Area.nombreArea,
                    cargoNombre = p.Cargo.nombreCargo,
                    activo = p.activo ? "Activo" : "Inactivo"
                })
                .ToListAsync();

        }

        public async Task<DetallePersonalDto> GetDetallePersonalAsync(int personalId) {
            var personal = await _context.Personales
                .Include(p => p.DatosPersonales)
                .Include(p => p.DatosPersonales.Usuario)
                .Where(i => i.personalId == personalId)
                .Select(i => new DetallePersonalDto
                {
                    personalId = personalId,
                    codUsuario = i.DatosPersonales.Usuario.codUsuario,
                    activo = i.activo,
                    rolId = i.DatosPersonales.Usuario.rolId,
                    nombres = i.DatosPersonales.nombres,
                    apellidoPaterno = i.DatosPersonales.apellidoPaterno,
                    apellidoMaterno = i.DatosPersonales.apellidoMaterno,
                    tipoDocumentoId = i.DatosPersonales.tipoDocumentoId,
                    numeroDocumento = i.DatosPersonales.numeroDocumento,
                    fechaNacimiento = i.DatosPersonales.fechaNacimiento ?? DateTime.MinValue,
                    ubigeoNacimiento = i.DatosPersonales.ubigeoNacimientoId,
                    ubigeoResidencia = i.DatosPersonales.ubigeoResidenciaId,
                    telefono = i.DatosPersonales.telefono,
                    emailPersonal = i.DatosPersonales.emailPersonal,
                    areaId = i.areaId,
                    cargoId = i.cargoId,
                    jefeDirectoId = i.jefeDirectoId ?? 0,
                    emailCorporativo = i.emailCorporativo,
                    fechaIngresoCompania = i.fechaIngresoCompania ?? DateTime.MinValue
                })
                .FirstOrDefaultAsync();
            if(personal == null)
            {
                throw new Exception("No existe el personal.");
            }

            return personal;
        }
    }
}
