using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

[ApiController]
[Route("Sunset-Sunrise")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherDataProvider _weatherDataProvider;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly WeatherApiContext _repository;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherDataProvider weatherDataProvider, IJsonProcessor jsonProcessor, WeatherApiContext weatherApiContext)
    {
        _logger = logger;
        _weatherDataProvider = weatherDataProvider;
        _jsonProcessor = jsonProcessor;
        _repository = weatherApiContext;
    }
    
    [HttpGet("Get")]
    public async Task<ActionResult<SunTime>> GetSunTime(string name)
    {
        try
        {
            // Check if the data already exists in the database
            var existingCity = await _repository.Cities.FirstOrDefaultAsync(c => c.Name == name);
            if (existingCity != null)
            {
                _logger.LogInformation($"Data for {name} already exists in the database.");
                // You may perform any necessary logging operations here
                return Ok(existingCity);
            }

            var weatherData = await _weatherDataProvider.GetCoordinates(name);
            var city = _jsonProcessor.CityProcess(weatherData);

            var sunsetSunrise = await _weatherDataProvider.GetSunTime(city.Latitude, city.Longitude);
            var time = _jsonProcessor.SunTimeProcess(sunsetSunrise);
            
            // Push data to the database

            var newCity = new City
            {
                Name = city.Name,
                Longitude = city.Longitude,
                Latitude = city.Latitude,
                State = city.State,
                Country = city.Country
            };
            
            _repository.Cities.Add(newCity);
            await _repository.SaveChangesAsync();
            
            var cityId = newCity.Id;
            
            // Push data to the database
            var newSunTime = new SunTime
            {
                CityId = cityId,
                SunRiseTime = time.SunRiseTime,
                SunSetTime = time.SunSetTime
            };
            
            _repository.SunTimes.Add(newSunTime);
            await _repository.SaveChangesAsync();

            return Ok(time);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sun data");
            return NotFound("Error getting sun data");
        }
    }
}