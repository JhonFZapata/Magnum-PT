using Magnum_PPT.Entities;
using Microsoft.EntityFrameworkCore;

namespace Magnum_PPT.Db
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets que representan tablas
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }

        // Configuraciones de rekaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación entre Game y PlayerOne
            modelBuilder.Entity<Game>()
                .HasOne(p => p.PlayerOne)
                .WithMany()
                .HasForeignKey(g => g.PlayerOneId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Game y PlayerTwo
            modelBuilder.Entity<Game>()
                .HasOne(p => p.PlayerTwo)
                .WithMany()
                .HasForeignKey(g => g.PlayerTwoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Game y Round
            modelBuilder.Entity<Round>()
                .HasOne(r => r.Game)
                .WithMany(g => g.Rounds)   
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
