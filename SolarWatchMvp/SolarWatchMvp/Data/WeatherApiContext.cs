using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunTime> SunTimes { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=solarwatch;User Id=sa;Password=yourStrong(!)Password;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // City configuration
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.OwnsOne(e => e.Coordinate);
            });

            // SunTime configuration
            modelBuilder.Entity<SunTime>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                // Other configurations for SunTime if necessary
            });
        }
    }
}