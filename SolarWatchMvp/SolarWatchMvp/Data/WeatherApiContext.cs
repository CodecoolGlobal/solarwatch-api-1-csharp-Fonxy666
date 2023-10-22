using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Data
{
    public class WeatherApiContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunTime> SunTimes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=solarwatch;User Id=sa;Password=yourStrong(!)Password;"
            );*/
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=solarwatch;User Id=sa;Password=yourStrong(!)Password;Encrypt=False;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cityCoordinate = new CityCoordinate { Id = 1, Longitude = 19.0402, Latitude = 47.4979 };
            
            //Configure the Coordinate entity-making the 'Id' unique
            modelBuilder.Entity<CityCoordinate>().HasIndex(u => u.Id).IsUnique();
            //Configure the City entity-making the 'Name' unique
            modelBuilder.Entity<City>().HasIndex(u => u.Name).IsUnique();
            
            modelBuilder.Entity<CityCoordinate>().HasData(cityCoordinate);

            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "Budapest",
                    CoordinateId = cityCoordinate.Id,
                    State = "Budapest",
                    Country = "Hungary"
                }
            );
            
            //Configure the SunTime entity-making the 'id' unique
            modelBuilder.Entity<SunTime>().HasIndex(u => u.Id).IsUnique();
            
            modelBuilder.Entity<SunTime>().HasData(
                new SunTime
                {
                    Id = 1,
                    SunRiseTime = "05:36",
                    SunSetTime = "18:32"
                }
            );
        }
    }
}