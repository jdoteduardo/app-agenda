using Agenda.Domain;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AgendaModel> Agendas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRefreshToken> UsuarioRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Agenda
            modelBuilder.Entity<AgendaModel>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<AgendaModel>()
                .Property(a => a.Nome)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<AgendaModel>()
                .Property(a => a.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<AgendaModel>()
                .Property(a => a.Telefone)
                .HasMaxLength(30);

            // Usuário
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Login)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Senha)
                .HasMaxLength(100)
                .IsRequired();

            // Usuário Refresh Token
            modelBuilder.Entity<UsuarioRefreshToken>()
                .HasKey(u => u.UsuarioId);

            modelBuilder.Entity<UsuarioRefreshToken>()
                .Property(u => u.DataExpiracao)
                .HasColumnType("datetime")
                .IsRequired();

            modelBuilder.Entity<UsuarioRefreshToken>()
                .Property(u => u.RefreshToken)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
