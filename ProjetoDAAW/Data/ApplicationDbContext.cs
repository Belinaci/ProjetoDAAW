using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoDAAW.Models;

namespace ProjetoDAAW.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProjetoDAAW.Models.Filme> Filme { get; set; } = default!;
        public DbSet<ProjetoDAAW.Models.Genero> Genero { get; set; } = default!;
        public DbSet<ProjetoDAAW.Models.Artista> Artista { get; set; } = default!;
        public DbSet<ProjetoDAAW.Models.Personagem> Personagem { get; set; } = default!;
    }
}
