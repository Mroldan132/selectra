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
        public async Task<Usuario> RegistrarAdministradorAsync(RegistrarAdministradorDto registroDto, int usuarioQueRegistraId)
        {
            await ValidarDatosComunesAsync(registroDto.CodUsuario, registroDto.TipoDocumentoId, registroDto.NumeroDocumento);
            await ValidarEmailCorporativoUnicoAsync(registroDto.EmailCorporativo);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {   
                var usuario = await CrearUsuarioAsync(
                    registroDto.CodUsuario,
                    registroDto.Clave,
                    registroDto.RolId,
                    registroDto.Activo,
                    usuarioQueRegistraId);

                var datosPersonales = await CrearDatosPersonalesAsync(registroDto, usuario.usuarioId, usuarioQueRegistraId);

                await transaction.CommitAsync();
                return usuario;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
        public async Task<Usuario> RegistrarPersonalAsync(RegistrarPersonalDto registroDto, int usuarioQueRegistraId)
        {
            await ValidarDatosComunesAsync(registroDto.CodUsuario, registroDto.TipoDocumentoId, registroDto.NumeroDocumento);
            await ValidarEmailCorporativoUnicoAsync(registroDto.EmailCorporativo);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var usuario = await CrearUsuarioAsync(
                    registroDto.CodUsuario,
                    registroDto.Clave,
                    registroDto.RolId,
                    registroDto.Activo,
                    usuarioQueRegistraId);

                var datosPersonales = await CrearDatosPersonalesAsync( registroDto, usuario.usuarioId, usuarioQueRegistraId);

                await CrearPersonalAsync(registroDto, datosPersonales.datosPersonalesId);

                await transaction.CommitAsync();
                return usuario;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }

        public async Task<Usuario> RegistrarAspiranteAsync(RegistrarAspiranteDto registroDto, int usuarioQueRegistraId)
        {
            await ValidarDatosComunesAsync(registroDto.CodUsuario, registroDto.TipoDocumentoId, registroDto.NumeroDocumento);

            using var transaction = await _context.Database.BeginTransactionAsync();
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.nivel == 0);
            if (rol == null) { 
                await transaction.RollbackAsync();
                throw new ApplicationException("No se encontró un rol de aspirante. Por favor, verifique la configuración de roles.");
            }
            try
            {
                var usuario = await CrearUsuarioAsync(
                    registroDto.CodUsuario,
                    registroDto.Clave,
                    rol.rolId,
                    registroDto.Activo,
                    usuarioQueRegistraId);

                var datosPersonales = await CrearDatosPersonalesAsync(registroDto, usuario.usuarioId, usuarioQueRegistraId);

                await CrearAspiranteAsync(registroDto, datosPersonales.datosPersonalesId);

                await transaction.CommitAsync();
                return usuario;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }

        private async Task ValidarDatosComunesAsync(string codUsuario, int tipoDocumentoId, string numeroDocumento)
        {
            if (await _context.Usuarios.AnyAsync(u => u.codUsuario == codUsuario))
            {
                throw new ApplicationException($"El código de usuario '{codUsuario}' ya existe.");
            }

            if (await _context.DatosPersonales.AnyAsync(dp => dp.tipoDocumentoId == tipoDocumentoId && dp.numeroDocumento == numeroDocumento))
            {
                throw new ApplicationException($"El documento '{tipoDocumentoId} - {numeroDocumento}' ya está registrado.");
            }
        }

        private async Task ValidarEmailCorporativoUnicoAsync(string emailCorporativo)
        {
            if (!string.IsNullOrWhiteSpace(emailCorporativo) &&
                await _context.Personales.AnyAsync(p => p.emailCorporativo == emailCorporativo))
            {
                throw new ApplicationException($"El email corporativo '{emailCorporativo}' ya está en uso.");
            }
        }

        private async Task<Usuario> CrearUsuarioAsync(string codUsuario, string clave, int rolId, bool activo, int usuarioQueRegistraId)
        {
            var ahora = DateTime.UtcNow;
            var usuario = new Usuario
            {
                codUsuario = codUsuario,
                claveHash = BCrypt.Net.BCrypt.HashPassword(clave), 
                rolId = rolId,
                activo = activo,
                fechaCreacion = ahora,
                fechaUltMod = ahora,
                usuarioUltModId = usuarioQueRegistraId
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        private async Task<Models.DatosPersonales> CrearDatosPersonalesAsync(IDatosPersonalesBasicosDto datosDto,int usuarioId , int usuarioQueRegistraId)
        {
            var ahora = DateTime.UtcNow;
            var datosPersonales = new Models.DatosPersonales 
            {
                apellidoPaterno = datosDto.ApellidoPaterno,
                apellidoMaterno = datosDto.ApellidoMaterno,
                usuarioId = usuarioId,
                nombres = datosDto.Nombres,
                tipoDocumentoId = datosDto.TipoDocumentoId,
                numeroDocumento = datosDto.NumeroDocumento,
                telefono = datosDto.Telefono,
                emailPersonal = datosDto.EmailPersonal,
                ubigeoNacimientoId = datosDto.UbigeoNacimiento,
                ubigeoResidenciaId = datosDto.UbigeoResidencia,
                fechaNacimiento = datosDto.FechaNacimiento, 
                fechaCreacion = ahora,
                fechaUltMod = ahora,
                usuarioUltModId = usuarioQueRegistraId,
            };
            _context.DatosPersonales.Add(datosPersonales);
            await _context.SaveChangesAsync();
            return datosPersonales;
        }

        private async Task<Personal> CrearPersonalAsync(RegistrarPersonalDto registroDto, int datosPersonalesId)
        {
            var ahora = DateTime.UtcNow;
            var personal = new Personal
            {
                datosPersonalesId = datosPersonalesId,
                emailCorporativo = registroDto.EmailCorporativo,
                areaId = registroDto.AreaId,
                cargoId = registroDto.CargoId,
                jefeDirectoId = registroDto.JefeDirectoId, 
                fechaIngresoCompania = registroDto.FechaIngresoCompania ?? ahora.Date,
                activo = registroDto.Activo
            };
            _context.Personales.Add(personal);
            await _context.SaveChangesAsync();
            return personal;
        }

        private async Task<Aspirantes> CrearAspiranteAsync(RegistrarAspiranteDto registroDto, int datosPersonalesId)
        {
            var ahora = DateTime.UtcNow; 
            var aspirante = new Aspirantes
            {
                datosPersonalesId = datosPersonalesId,
                nivelAcademicoId = registroDto.NivelAcademicoId,
                pathCV = registroDto.PathCV,
                pathFoto = registroDto.PathFoto,
            };
            _context.Aspirantes.Add(aspirante);
            await _context.SaveChangesAsync();
            return aspirante;
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

            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
            }
            return usuario;
        }
        public async Task<List<ListaRolesDto>> GetListaRolesAync() =>
            await _context.Roles
                .Select(r => new ListaRolesDto
                {
                    RolId= r.rolId,
                    Nombre = r.nombreRol
                })
                .ToListAsync();
    }
}
