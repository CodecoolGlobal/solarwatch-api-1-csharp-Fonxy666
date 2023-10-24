using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Data
{
    public class WeatherApiContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunTime> SunTimes { get; set; }
        
        public WeatherApiContext(DbContextOptions<WeatherApiContext> options) : base(options)
        {
        }

        public WeatherApiContext()
        {
            
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var city = new City
            {
                Id = 1, Name = "Budapest", Longitude = 19.0402,Latitude = 47.4979,State = "Budapest", Country = "Hungary"
            };
            
            // Configure the City entity-making the 'Name' unique
            modelBuilder.Entity<City>().HasIndex(u => u.Name).IsUnique();
            
            modelBuilder.Entity<City>().HasData(city);
            
            // Configure the SunTime entity-making the 'Id' unique
            modelBuilder.Entity<SunTime>().HasIndex(u => u.Id).IsUnique();

            modelBuilder.Entity<SunTime>().HasData(
                new SunTime { Id = 1, CityId = city.Id, SunRiseTime = "05:36", SunSetTime = "18:32" }
                );
        }*/
    }
}