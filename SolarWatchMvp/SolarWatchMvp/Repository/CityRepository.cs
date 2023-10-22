using SolarWatchMvp.Data;

namespace SolarWatchMvp.Repository;

public class CityRepository : ICityRepository
{
    public IEnumerable<City> GetAll()
    {
        using var dbContext = new WeatherApiContext();
        return dbContext.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        using var dbContext = new WeatherApiContext();
        return dbContext.Cities.FirstOrDefault(city => city.Name == name);
    }

    public void Add(City city)
    {
        using var dbContext = new WeatherApiContext();
        dbContext.Cities.Add(city);
        dbContext.SaveChanges();
    }

    public void Delete(City city)
    {
        using var dbContext = new WeatherApiContext();
        dbContext.Cities.Remove(city);
        dbContext.SaveChanges();
    }

    public void Update(City city)
    {
        using var dbContext = new WeatherApiContext();
        dbContext.Cities.Update(city);
        dbContext.SaveChanges();
    }
}