using System.Data.Entity;

namespace EncuestasMVC.Models
{
    public class EncuestasDbContext : DbContext
    {
        public EncuestasDbContext() : base("EncuestasDb") { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Campo> Campos { get; set; }
        public DbSet<RespuestaCampo> Respuestas { get; set; }


    }
}
