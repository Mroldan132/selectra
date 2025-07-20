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
        public DbSet<PreguntasFiltros> PreguntasFiltros { get; set; }
        public DbSet<OpcionPreguntaFiltro> OpcionesPreguntasFiltros { get; set; }
        public DbSet<TipoPreguntasFiltro> TipoPreguntasFiltros { get; set; }
        public DbSet<TiposRequerimiento> TiposRequerimientos { get; set; }
        public DbSet<EstadoHistorialAprobacion> EstadosHistorialAprobaciones { get; set; }
        public DbSet<EstadoOfertaLaboral> EstadosOfertaLaborales { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<EstadoRequerimiento> EstadosRequerimientos { get; set; }
        public DbSet<NotificacionesUsuarios> NotificacionesUsuarios { get; set; }
        public DbSet<NivelAcademicos> NivelAcademicos { get; set; }
        public DbSet<Aspirantes> Aspirantes { get; set; }
        public DbSet<Ubigeo> Ubigeos { get; set; }
        public DbSet<SolicitudVacaciones> SolicitudVacaciones { get; set; }
        public DbSet<EstadoSolicitudVacaciones> EstadoSolicitudVacaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Personal>()
                .HasMany(p => p.Solicitudes)              
                .WithOne(s => s.Personal)                 
                .HasForeignKey(s => s.personalId)         
                .OnDelete(DeleteBehavior.Cascade);        

            modelBuilder.Entity<SolicitudVacaciones>()
                .HasOne(s => s.Aprobador)                  
                .WithMany()                                
                .HasForeignKey(s => s.AprobadorId)         
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoPreguntasFiltro>()
            .ToTable("TipoPreguntasFiltros");
            
            modelBuilder.Entity<TipoPreguntasFiltro>().HasData(
                new TipoPreguntasFiltro { tipoPreguntaId = 1, nombre = "Conocimientos Generales" },
                new TipoPreguntasFiltro { tipoPreguntaId = 2, nombre = "Aptitudes Técnicas" },
                new TipoPreguntasFiltro { tipoPreguntaId = 3, nombre = "Habilidades Blandas" }
         );
        }
    }
}
