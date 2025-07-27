using Microsoft.EntityFrameworkCore;
using SpaceExplorer.API.Models;

namespace SpaceExplorer.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AIResult> AIResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Planet>()
                .HasOne(p => p.AIResult)
                .WithOne(a => a.Planet)
                .HasForeignKey<AIResult>(a => a.PlanetID);
        }

        

        
    }
}