using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Repository;

public class SunTimeRepository : ISunTimeRepository
{
    public IEnumerable<SunTime> GetAll()
    {
        using var dbContext = new WeatherApiContext();
        return dbContext.SunTimes.ToList();
    }

    public SunTime? GetByName(int id)
    {
        using var dbContext = new WeatherApiContext();
        return dbContext.SunTimes.FirstOrDefault(time => time.Id == id);
    }

    public void Add(SunTime time)
    {
        using var dbContext = new WeatherApiContext();
        dbContext.SunTimes.Add(time);
    }

    public void Delete(SunTime time)
    {
        using var dbContext = new WeatherApiContext();
        dbContext.SunTimes.Remove(time);
    }

    public void Update(SunTime time)
    {
        using var dbContext = new WeatherApiContext();
        dbContext.SunTimes.Update(time);
        dbContext.SaveChanges();
    }
}