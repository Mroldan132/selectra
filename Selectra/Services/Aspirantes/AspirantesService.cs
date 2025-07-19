using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Aspirantes
{
    public class AspirantesService : IAspirantesService
    {
        private readonly SelectraContext _context;

        public AspirantesService(SelectraContext context)
        {
            _context = context;
        }

        public async Task<DetalleAspiranteDto> GetDetalleAspiranteAsync(int aspiranteId)
        {
            var aspirante = await _context.Aspirantes
                .Include(p => p.DatosPersonales)
                .Include(p => p.DatosPersonales.Usuario)
                .Where(i => i.aspiranteId == aspiranteId)
            .Select(i => new DetalleAspiranteDto
            {
                    aspiranteId = aspiranteId,
                    codUsuario = i.DatosPersonales.Usuario.codUsuario,
                    estado = i.estado,
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
                    fechaCreacion = i.fechaCreacion,
                    fechaUltMod = i.fechaUltMod,
                    pathCV = i.pathCV,
                    pathFoto = i.pathFoto,
                    NivelAcademico = i.NivelAcademico.nombre
            })
                .FirstOrDefaultAsync();
            if (aspirante == null)
            {
                throw new Exception("No existe el aspirante.");
            }

            return aspirante;
        }

        public async Task<IEnumerable<ListaAspirantesDto>> GetListaAspirantesAsync()
        {
            return await _context.Aspirantes
                .Include(p => p.DatosPersonales)
                .Include(p => p.NivelAcademico)
                .Select(p => new ListaAspirantesDto
                {
                    aspiranteId = p.aspiranteId,
                    codUsuario = p.DatosPersonales.Usuario.codUsuario,
                    nombres = p.DatosPersonales.nombres,
                    apellidoPaterno = p.DatosPersonales.apellidoPaterno,
                    apellidoMaterno = p.DatosPersonales.apellidoMaterno,
                    nivelAcademicoNombre = p.NivelAcademico.nombre,
                    estado = p.estado ? "Activo" : "Inactivo"
                })
                .ToListAsync();
        }
    }
}
