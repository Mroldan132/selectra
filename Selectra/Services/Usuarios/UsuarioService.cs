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

        private async Task<Models.Aspirantes> CrearAspiranteAsync(RegistrarAspiranteDto registroDto, int datosPersonalesId)
        {
            var ahora = DateTime.UtcNow;
            var aspirante = new Models.Aspirantes // Ensure the correct namespace is used  
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
        public async Task<bool> VerificarExisteUsuario(string usuario) =>
            await _context.Usuarios
                .AnyAsync(u => u.codUsuario.ToLower() == usuario.ToLower());

        public async Task<bool> ActualizarPersonal(ActualizarPersonalDto personalDto, int personalId, int usuarioQueModificaId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var personal = await _context.Personales
                    .Include(p => p.DatosPersonales)
                    .Include(p => p.DatosPersonales.Usuario)
                    .FirstOrDefaultAsync(p => p.personalId == personalId);

                // Si no se encuentra el personal, lanzar una excepción.
                if (personal == null || personal.DatosPersonales == null || personal.DatosPersonales.Usuario == null)
                {
                    throw new KeyNotFoundException($"No se encontró el registro de personal con el ID {personalId}.");
                }

                // --- VALIDACIONES ---
                // Validar que el nuevo email corporativo no esté en uso por OTRO personal.
                if (personal.emailCorporativo != personalDto.EmailCorporativo)
                {
                    if (await _context.Personales.AnyAsync(p => p.emailCorporativo == personalDto.EmailCorporativo && p.personalId != personalId))
                    {
                        throw new ApplicationException($"El email corporativo '{personalDto.EmailCorporativo}' ya está en uso.");
                    }
                }

                // Validar que el nuevo número de documento no esté en uso por OTRA persona.
                if (personal.DatosPersonales.numeroDocumento != personalDto.NumeroDocumento)
                {
                    if (await _context.DatosPersonales.AnyAsync(dp => dp.tipoDocumentoId == personalDto.TipoDocumentoId && dp.numeroDocumento == personalDto.NumeroDocumento && dp.datosPersonalesId != personal.datosPersonalesId))
                    {
                        throw new ApplicationException($"El documento '{personalDto.NumeroDocumento}' ya está registrado.");
                    }
                }


                // --- ACTUALIZACIÓN DE ENTIDADES ---
                var ahora = DateTime.UtcNow;
                var usuario = personal.DatosPersonales.Usuario;
                var datosPersonales = personal.DatosPersonales;

                // 3. Actualizar la entidad Usuario
                usuario.rolId = personalDto.RolId;
                usuario.activo = personalDto.Activo;
                usuario.fechaUltMod = ahora;
                usuario.usuarioUltModId = usuarioQueModificaId;

                // Solo actualizar la contraseña si se proporcionó una nueva.
                if (!string.IsNullOrWhiteSpace(personalDto.Clave))
                {
                    usuario.claveHash = BCrypt.Net.BCrypt.HashPassword(personalDto.Clave);
                }

                // 4. Actualizar la entidad DatosPersonales
                datosPersonales.nombres = personalDto.Nombres;
                datosPersonales.apellidoPaterno = personalDto.ApellidoPaterno;
                datosPersonales.apellidoMaterno = personalDto.ApellidoMaterno;
                datosPersonales.tipoDocumentoId = personalDto.TipoDocumentoId;
                datosPersonales.numeroDocumento = personalDto.NumeroDocumento;
                datosPersonales.emailPersonal = personalDto.EmailPersonal;
                datosPersonales.telefono = personalDto.Telefono;
                datosPersonales.ubigeoNacimientoId = personalDto.UbigeoNacimiento;
                datosPersonales.ubigeoResidenciaId = personalDto.UbigeoResidencia;
                datosPersonales.fechaNacimiento = personalDto.FechaNacimiento;
                datosPersonales.fechaUltMod = ahora;
                datosPersonales.usuarioUltModId = usuarioQueModificaId;

                personal.emailCorporativo = personalDto.EmailCorporativo;
                personal.areaId = personalDto.AreaId;
                personal.cargoId = personalDto.CargoId;
                personal.jefeDirectoId = personalDto.JefeDirectoId;
                personal.fechaIngresoCompania = personalDto.FechaIngresoCompania;
                personal.activo = personalDto.Activo;

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }

        public async Task<bool> ActualizarAspirante(ActualizarAspiranteDto aspiranteDto, int personalId, int usuarioQueModificaId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var personal = await _context.Personales
                    .Include(p => p.DatosPersonales)
                    .Include(p => p.DatosPersonales.Usuario)
                    .FirstOrDefaultAsync(p => p.personalId == personalId);

                // Si no se encuentra el personal, lanzar una excepción.
                if (personal == null || personal.DatosPersonales == null || personal.DatosPersonales.Usuario == null)
                {
                    throw new KeyNotFoundException($"No se encontró el registro de personal con el ID {personalId}.");
                }

                // --- VALIDACIONES ---
                // Validar que el nuevo email corporativo no esté en uso por OTRO personal.
                if (personal.emailCorporativo != aspiranteDto.EmailCorporativo)
                {
                    if (await _context.Personales.AnyAsync(p => p.emailCorporativo == aspiranteDto.EmailCorporativo && p.personalId != personalId))
                    {
                        throw new ApplicationException($"El email corporativo '{aspiranteDto.EmailCorporativo}' ya está en uso.");
                    }
                }

                // Validar que el nuevo número de documento no esté en uso por OTRA persona.
                if (personal.DatosPersonales.numeroDocumento != aspiranteDto.NumeroDocumento)
                {
                    if (await _context.DatosPersonales.AnyAsync(dp => dp.tipoDocumentoId == aspiranteDto.TipoDocumentoId && dp.numeroDocumento == personalDto.NumeroDocumento && dp.datosPersonalesId != personal.datosPersonalesId))
                    {
                        throw new ApplicationException($"El documento '{aspiranteDto.NumeroDocumento}' ya está registrado.");
                    }
                }


                // --- ACTUALIZACIÓN DE ENTIDADES ---
                var ahora = DateTime.UtcNow;
                var usuario = personal.DatosPersonales.Usuario;
                var datosPersonales = personal.DatosPersonales;

                // 3. Actualizar la entidad Usuario
                usuario.rolId = aspiranteDto.RolId;
                usuario.activo = aspiranteDto.Activo;
                usuario.fechaUltMod = ahora;
                usuario.usuarioUltModId = usuarioQueModificaId;

                // Solo actualizar la contraseña si se proporcionó una nueva.
                if (!string.IsNullOrWhiteSpace(aspiranteDto.Clave))
                {
                    usuario.claveHash = BCrypt.Net.BCrypt.HashPassword(aspiranteDto.Clave);
                }

                // 4. Actualizar la entidad DatosPersonales
                datosPersonales.nombres = aspiranteDto.Nombres;
                datosPersonales.apellidoPaterno = aspiranteDto.ApellidoPaterno;
                datosPersonales.apellidoMaterno = aspiranteDto.ApellidoMaterno;
                datosPersonales.tipoDocumentoId = aspiranteDto.TipoDocumentoId;
                datosPersonales.numeroDocumento = aspiranteDto.NumeroDocumento;
                datosPersonales.emailPersonal = aspiranteDto.EmailPersonal;
                datosPersonales.telefono = aspiranteDto.Telefono;
                datosPersonales.ubigeoNacimientoId = aspiranteDto.UbigeoNacimiento;
                datosPersonales.ubigeoResidenciaId = aspiranteDto.UbigeoResidencia;
                datosPersonales.fechaNacimiento = aspiranteDto.FechaNacimiento;
                datosPersonales.fechaUltMod = ahora;
                datosPersonales.usuarioUltModId = usuarioQueModificaId;

                personal.emailCorporativo = aspiranteDto.EmailCorporativo;
                personal.areaId = aspiranteDto.AreaId;
                personal.cargoId = aspiranteDto.CargoId;
                personal.jefeDirectoId = aspiranteDto.JefeDirectoId;
                personal.fechaIngresoCompania = aspiranteDto.FechaIngresoCompania;
                personal.activo = aspiranteDto.Activo;

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
