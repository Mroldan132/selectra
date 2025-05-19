using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SelectraContext _context;
        public UsuarioService(SelectraContext context)
        {
            _context = context;
        }
        public async Task<Usuario> RegistrarUsuarioAsync(RegistrarUsuarioDto registroDto, int usuarioQueRegistraId)
        {
            if (await _context.Usuarios.AnyAsync(u => u.codUsuario == registroDto.CodUsuario))
            {
                throw new ApplicationException($"El código de usuario '{registroDto.CodUsuario}' ya existe.");
            }

            if (await _context.DatosPersonales.AnyAsync(dp => dp.tipoDocumentoId == registroDto.TipoDocumentoId && dp.numeroDocumento == registroDto.NumeroDocumento))
            {
                throw new ApplicationException($"El documento '{registroDto.TipoDocumentoId} - {registroDto.NumeroDocumento}' ya está registrado.");
            }

            if (await _context.Personales.AnyAsync(p => p.emailCorporativo == registroDto.EmailCorporativo))
            {
                throw new ApplicationException($"El email corporativo '{registroDto.EmailCorporativo}' ya está en uso.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var ahora = DateTime.UtcNow;

                var usuario = new Usuario
                {
                    codUsuario = registroDto.CodUsuario,
                    claveHash = BCrypt.Net.BCrypt.HashPassword(registroDto.Clave),
                    rolId = registroDto.RolId,
                    activo = registroDto.Activo,
                    fechaCreacion = ahora,
                    fechaUltMod = ahora,
                    usuarioUltModId = usuarioQueRegistraId
                };
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();


                var datosPersonales = new DatosPersonales
                {
                    apellidoPaterno = registroDto.ApellidoPaterno,
                    apellidoMaterno = registroDto.ApellidoMaterno,
                    nombres = registroDto.Nombres,
                    tipoDocumentoId = registroDto.TipoDocumentoId,
                    numeroDocumento = registroDto.NumeroDocumento,
                    telefono = registroDto.Telefono,
                    emailPersonal = registroDto.EmailPersonal,
                    ubigeoNacimiento = registroDto.UbigeoNacimiento,
                    ubigeoResidencia = registroDto.UbigeoResidencia,
                    fechaNacimiento = registroDto.FechaNacimiento,
                    fechaCreacion = ahora,
                    fechaUltMod = ahora,
                    usuarioUltModId = usuarioQueRegistraId
                };
                _context.DatosPersonales.Add(datosPersonales);
                await _context.SaveChangesAsync();

                var personal = new Personal
                {
                    datosPersonalesId = datosPersonales.datosPersonalesId,
                    usuarioId = usuario.usuarioId,
                    emailCorporativo = registroDto.EmailCorporativo,
                    areaId = registroDto.AreaId,
                    cargoId = registroDto.CargoId,
                    jefeDirectoId = registroDto.JefeDirectoId,
                    fechaIngresoCompania = registroDto.FechaIngresoCompania ?? ahora.Date,
                    activo = registroDto.Activo,
                };
                _context.Personales.Add(personal);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return usuario; 
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<UsuarioDetalleDto> GetUsuarioPorIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.usuarioId == id)
                .Include(u => u.Rol)
                .Select(u => new UsuarioDetalleDto
                {
                    UsuarioId = u.usuarioId,
                    CodUsuario = u.codUsuario,
                    Activo = u.activo,
                    NombreRol = u.Rol != null ? u.Rol.nombreRol : string.Empty, 
                    FechaCreacion = u.fechaCreacion,
                    FechaUltMod = u.fechaUltMod
                })
                .SingleOrDefaultAsync();

            return usuario;
        }
    }
}
