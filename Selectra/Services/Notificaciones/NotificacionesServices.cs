using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Selectra.DTOs;
using Selectra.Models;

namespace Selectra.Services.Notificaciones
{
    public class NotificacionesServices : INotificacionesServices
    {
        private readonly SelectraContext _context;

        public NotificacionesServices(SelectraContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearNotificacionAsync(CrearNotificacionDto notificacion)
        {
            var nuevaNotificacion = new NotificacionesUsuarios
            {
                usuarioId = notificacion.usuarioId,
                titulo = notificacion.titulo,
                mensaje = notificacion.mensaje,
                tipo = notificacion.tipo,
                fechaCreacion = DateTime.Now,
                leida = false,
                link = notificacion.link
            };
            await _context.NotificacionesUsuarios.AddAsync(nuevaNotificacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<NotificacionesResponseDto> GetListNotificacionesAsync(int usuarioId)
        {
            var notificaciones = await _context.NotificacionesUsuarios
                .Where(n => n.usuarioId == usuarioId && n.estado)
                .OrderByDescending(n => n.fechaCreacion)
                .ToListAsync();
            var unreadCount = await _context.NotificacionesUsuarios.CountAsync(n => n.usuarioId == usuarioId && !n.leida);

            return new NotificacionesResponseDto { Items = notificaciones, UnreadCount = unreadCount };
        }

        public async Task<bool> LeerNotificiacionAsync(int notificacionId, int usuarioId)
        {
            var notificacion = await _context.NotificacionesUsuarios
                .FirstOrDefaultAsync(n => n.notificacionId == notificacionId && n.usuarioId == usuarioId);
            if (notificacion != null)
            {
                notificacion.leida = true;
                _context.NotificacionesUsuarios.Update(notificacion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> MarcarTodasLeidasAsync(int usuarioId)
        {
            var notificaciones = await _context.NotificacionesUsuarios
                .Where(n => n.usuarioId == usuarioId)
                .ToListAsync();

            if(notificaciones.Count() > 0)
            {
                foreach (var item in notificaciones)
                {
                    item.leida = true;
                    _context.NotificacionesUsuarios.Update(item);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            return false;
                
        }
        public async Task<bool> CrearNotificacionNuevoRequerimientoAsync(RequerimientoCreadoDto requerimientoCreadoDto)
        {

            if (requerimientoCreadoDto?.AprobadorId is int aprobadorId)
            {
                var personal = await _context.Personales
                    .FirstOrDefaultAsync(i => i.personalId == requerimientoCreadoDto.AprobadorId);
                if (personal == null)
                {
                    return false;
                }

                var notificacion = new CrearNotificacionDto
                {
                    usuarioId = personal.usuarioId,
                    titulo = "Nuevo Requerimiento",
                    mensaje = $"Se ha creado un nuevo requerimiento: '{requerimientoCreadoDto.TituloRequerimiento}' (ID {requerimientoCreadoDto.RequerimientoId}).",
                    tipo = "Requerimiento",
                    link = $"/requerimientos?accion=aprobar&idRequerimiento={requerimientoCreadoDto.RequerimientoId}"
                };
                await CrearNotificacionAsync(notificacion);
                return true;
            }
            return false;
        }
        
        public async Task<bool> CrearNotificacionRequerimientoAprobadoRechazado(AprobarRechazarRequerimientoDto aprobarRechazardto)
        {
            var requerimiento = await _context.RequerimientosPersonales
                .FirstOrDefaultAsync(e => e.requerimientoId == aprobarRechazardto.idRequerimiento);

            var estadoRequerimiento = await _context.EstadosRequerimientos
                .FirstOrDefaultAsync(e => e.codigoEstado == aprobarRechazardto.CodigoEstado);

            if(requerimiento == null)
            {
                return false;
            }

            var personal = await _context.Personales
                .FirstOrDefaultAsync(e => e.personalId == requerimiento.solicitanteId);

            if(personal == null)
            {
                return false;
            }
            var notificacion = new CrearNotificacionDto
            {
                usuarioId = personal.usuarioId,
                titulo = $"Requerimiento {estadoRequerimiento?.nombreEstado}",
                mensaje = aprobarRechazardto.Observaciones,
                tipo = estadoRequerimiento.codigoEstado.Equals("APR") ? "RequerimientoAprobado" : "RequerimientoRechazado",
                link = $"/requerimientos?accion=detalle&idRequerimiento={aprobarRechazardto.idRequerimiento}"
            };
            await CrearNotificacionAsync(notificacion);
            return true;
        }
    }
}
