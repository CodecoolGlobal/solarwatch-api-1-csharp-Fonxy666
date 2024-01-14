using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Repository;

public class SunTimeRepository(WeatherApiContext context) : ISunTimeRepository
{
    public IEnumerable<SunTime> GetAll()
    {
        return context.SunTimes!.ToList();
    }

    public SunTime? GetByName(int id)
    {
        return context.SunTimes!.FirstOrDefault(time => time.Id == id);
    }

    public void Add(SunTime time)
    {
        context.SunTimes!.Add(time);
    }

    public void Delete(SunTime time)
    {
        context.SunTimes!.Remove(time);
    }

    public void Update(SunTime time)
    {
        context.SunTimes!.Update(time);
        context.SaveChanges();
    }
}