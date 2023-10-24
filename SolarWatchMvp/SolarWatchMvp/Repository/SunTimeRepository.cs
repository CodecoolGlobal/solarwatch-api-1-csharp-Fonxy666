using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Repository;

public class SunTimeRepository : ISunTimeRepository
{
    
    private readonly WeatherApiContext _context;

    public SunTimeRepository(WeatherApiContext context)
    {
        _context = context;
    }
    
    public IEnumerable<SunTime> GetAll()
    {
        return _context.SunTimes.ToList();
    }

    public SunTime? GetByName(int id)
    {
        return _context.SunTimes.FirstOrDefault(time => time.Id == id);
    }

    public void Add(SunTime time)
    {
        _context.SunTimes.Add(time);
    }

    public void Delete(SunTime time)
    {
        _context.SunTimes.Remove(time);
    }

    public void Update(SunTime time)
    {
        _context.SunTimes.Update(time);
        _context.SaveChanges();
    }
}