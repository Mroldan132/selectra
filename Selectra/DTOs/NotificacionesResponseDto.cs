using Selectra.Models;

namespace Selectra.DTOs
{
    public class NotificacionesResponseDto
    {
        public List<NotificacionesUsuarios> Items { get; set; }
        public int UnreadCount { get; set; }
    }
}
