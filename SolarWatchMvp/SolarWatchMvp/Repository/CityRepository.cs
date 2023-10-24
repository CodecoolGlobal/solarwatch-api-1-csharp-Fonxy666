using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Repository;

public class CityRepository : ICityRepository
{
    private readonly WeatherApiContext _context;

    public CityRepository(WeatherApiContext context)
    {
        _context = context;
    }
    public IEnumerable<City> GetAll()
    {
        return _context.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        return _context.Cities.FirstOrDefault(city => city.Name == name);
    }

    public void Add(City city)
    {
        _context.Cities.Add(city);
        _context.SaveChanges();
    }

    public void Delete(City city)
    {
        _context.Cities.Remove(city);
        _context.SaveChanges();
    }

    public void Update(City city)
    {
        _context.Cities.Update(city);
        _context.SaveChanges();
    }
}