using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParticipacionDigital.Core.Entities;

namespace ParticipacionDigital.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ParticipacionDigitalDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<Encuesta> Encuestas { get; set; } = null!;
        public DbSet<Opcion> Opciones { get; set; } = null!;
        public DbSet<Voto> Votos { get; set; } = null!;
        public DbSet<Comentario> Comentarios { get; set; } = null!;
        public DbSet<Inquietud> Inquietudes { get; set; } = null!;
        public DbSet<RespuestaInquietud> RespuestasInquietudes { get; set; } = null!;
        public DbSet<Alcaldia> Alcaldias { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<Actividad> Actividades { get; set; } = null!;
        public DbSet<Amonestacion> Amonestaciones { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relación Usuario -> Encuestas (Creador)
            builder.Entity<Encuesta>()
                .HasOne(e => e.Creador)
                .WithMany(u => u.EncuestasCreadas)
                .HasForeignKey(e => e.CreadorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Voto -> Usuario
            builder.Entity<Voto>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Votos)
                .HasForeignKey(v => v.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Voto -> Encuesta
            builder.Entity<Voto>()
                .HasOne(v => v.Encuesta)
                .WithMany(e => e.Votos)
                .HasForeignKey(v => v.EncuestaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Comentario -> Usuario
            builder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

             // Relación Comentario -> Encuesta
            builder.Entity<Comentario>()
                .HasOne(c => c.Encuesta)
                .WithMany(e => e.Comentarios)
                .HasForeignKey(c => c.EncuestaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Inquietud -> Autor
            builder.Entity<Inquietud>()
                .HasOne(i => i.Autor)
                .WithMany()
                .HasForeignKey(i => i.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación RespuestaInquietud -> Autor
            builder.Entity<RespuestaInquietud>()
                .HasOne(r => r.Autor)
                .WithMany()
                .HasForeignKey(r => r.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación RespuestaInquietud -> Inquietud
            builder.Entity<RespuestaInquietud>()
                .HasOne(r => r.Inquietud)
                .WithMany(i => i.Respuestas)
                .HasForeignKey(r => r.InquietudId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Usuario -> Alcaldia
            builder.Entity<Usuario>()
                .HasOne(u => u.Alcaldia)
                .WithMany(a => a.Usuarios)
                .HasForeignKey(u => u.AlcaldiaId)
                .OnDelete(DeleteBehavior.SetNull);

            // Relación Encuesta -> Alcaldia
            builder.Entity<Encuesta>()
                .HasOne(e => e.Alcaldia)
                .WithMany(a => a.Encuestas)
                .HasForeignKey(e => e.AlcaldiaId)
                .OnDelete(DeleteBehavior.SetNull);

            // Relación Amonestacion -> Usuario (Sancionado)
            builder.Entity<Amonestacion>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Amonestaciones)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Amonestacion -> Admin (Sancionador)
            builder.Entity<Amonestacion>()
                .HasOne(a => a.Admin)
                .WithMany()
                .HasForeignKey(a => a.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Amonestacion -> AdminLevantamiento (Perdonador)
            builder.Entity<Amonestacion>()
                .HasOne(a => a.AdminLevantamiento)
                .WithMany()
                .HasForeignKey(a => a.AdminLevantamientoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class AppDbContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ParticipacionDigitalDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }

}
