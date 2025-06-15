using Microsoft.EntityFrameworkCore;
using SpaceExplorer.API.Models;

namespace SpaceExplorer.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Planet> Planets { get; set; }
    }
}