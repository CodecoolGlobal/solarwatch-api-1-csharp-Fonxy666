using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Data;

public class WeatherApiContext : DbContext
{
    public DbSet<City>? Cities { get; set; }
    public DbSet<SunTime>? SunTimes { get; set; }
    
    public WeatherApiContext(DbContextOptions<WeatherApiContext> options) : base(options)
    {
    }

    public WeatherApiContext()
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }
    }
}
