using IronFit.Models;
using Microsoft.EntityFrameworkCore;

namespace IronFit.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Treino> Treinos { get; set; }
    }
}
