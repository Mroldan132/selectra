using Microsoft.EntityFrameworkCore;

namespace Selectra.Models
{
    public class SelectraContext : DbContext
    {
        public SelectraContext(DbContextOptions<SelectraContext> options) : base(options)
        {

        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<DatosPersonales> DatosPersonales { get; set; }
        public DbSet<EstadoPostulante> EstadosPostulantes { get; set; }
        public DbSet<HistorialAprobacion> HistorialAprobaciones { get; set; }
        public DbSet<OfertaLaboral> OfertasLaborales { get; set; }
        public DbSet<Personal> Personales { get; set; }
        public DbSet<Postulante> Postulantes { get; set; }
        public DbSet<RequerimientoPersonal> RequerimientosPersonales { get; set; }
        public DbSet<RespuestaPostulante> RespuestasPostulantes { get; set; }
        public DbSet<TipoDocumento> TiposDocumentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<OrdenAprobacion> OrdenesAprobaciones { get; set; }
        public DbSet<PreguntaFiltro> PreguntasFiltros { get; set; }
        public DbSet<TiposRequerimiento> TiposRequerimientos { get; set; }
        public DbSet<EstadoHistorialAprobacion> EstadosHistorialAprobaciones { get; set; }
        public DbSet<EstadoOfertaLaboral> EstadosOfertaLaborales { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<EstadoRequerimiento> EstadosRequerimientos { get; set; }
        public DbSet<NotificacionesUsuarios> NotificacionesUsuarios { get; set; }



    }
}
