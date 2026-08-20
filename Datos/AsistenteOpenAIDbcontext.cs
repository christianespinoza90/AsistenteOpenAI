using Microsoft.EntityFrameworkCore;
using AsistenteOpenAI.Models;

namespace AsistenteOpenAI.Datos
{
    public class AsistenteDbContext : DbContext
    {
        // 1er paso: DbSet para las entidades
        public DbSet<PreguntaIA> Preguntas { get; set; }
        public DbSet<RespuestaIA> Respuestas { get; set; }

        // 2do paso: Configurar la cadena de conexión
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Conexión por Usuario de SQL Server (Ajusta Server, User Id y Password)
                optionsBuilder.UseSqlServer(@"Server=CHRISTIAN2025\SQLEXPRESS; Database=AsistenteAI; Trusted_Connection=True; TrustServerCertificate=True;");
            }

            // Conexión por Autenticación de Windows:
            // optionsBuilder.UseSqlServer("Christian2025\\SQLEXPRESS; Database=AsistenteAI; Trusted_Connection=True; TrustServerCertificate=True;");
        }
        

        // 3er paso: Configurar la relación entre Preguntas y Respuestas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación 1 a 1 (Una Pregunta tiene una Respuesta)
            modelBuilder.Entity<PreguntaIA>()
                .HasOne(p => p.Respuesta)
                .WithOne(r => r.PreguntaIA)
                .HasForeignKey<RespuestaIA>(r => r.PreguntaIAId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}