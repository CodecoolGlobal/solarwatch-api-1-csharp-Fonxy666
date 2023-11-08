using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

[ApiController]
[Route("[controller]")]
public class CrudAdminController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherDataProvider _weatherDataProvider;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly WeatherApiContext _repository;

    public CrudAdminController(ILogger<WeatherForecastController> logger, IWeatherDataProvider weatherDataProvider, IJsonProcessor jsonProcessor, WeatherApiContext weatherApiContext)
    {
        _logger = logger;
        _weatherDataProvider = weatherDataProvider;
        _jsonProcessor = jsonProcessor;
        _repository = weatherApiContext;
    }

    [HttpPut("Put"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<SunTime>> Put(
        int id,
        string incomingCityName,
        double incomingCityLongitude,
        double incomingCityLatitude,
        string incomingCityState,
        string incomingCityCountry,
        string incomingSunRiseTime,
        string incomingSunSetTime
        )
    {
        try
        {
            var existingCity = await _repository.Cities!.FirstOrDefaultAsync(city => city.Id == id);
            if (existingCity == null)
            {
                _logger.LogInformation($"Data for id: {id} doesn't exists in the database.");
                return Ok(existingCity);
            }
            else
            {
                existingCity.ChangeCityData(incomingCityName,
                    incomingCityLongitude,
                    incomingCityLatitude,
                    incomingCityState,
                    incomingCityCountry);
            }
            
            var existingSunTime = await _repository.SunTimes!.FirstOrDefaultAsync(sunTime => sunTime.CityId == existingCity.Id);
            if (existingSunTime == null)
            {
                return Ok(existingSunTime);
            }
            else
            {
                existingSunTime.ChangeSunTimeData(incomingSunRiseTime, incomingSunSetTime);
            }
            
            await _repository.SaveChangesAsync();
            
            return Ok($"City with id: {id} successfully updated!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error put sun data with id: {id}");
            return NotFound($"Error put sun data with id: {id}");
        }
    }

    [HttpDelete("Delete"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<SunTime>> Delete(int id)
    {
        try
        {
            var existingCity = await _repository.Cities!.FirstOrDefaultAsync(city => city.Id == id);
            if (existingCity == null)
            {
                _logger.LogInformation($"Data for id: {id} doesnt't exists in the database.");
                return Ok(existingCity);
            }
            
            var existingSunTime = await _repository.SunTimes!.FirstOrDefaultAsync(sunTime => sunTime.CityId == existingCity.Id);
            if (existingSunTime == null)
            {
                return Ok(existingCity);
            }

            _repository.Cities!.Remove(existingCity);
            _repository.SunTimes!.Remove(existingSunTime);
            await _repository.SaveChangesAsync();

            return Ok($"Successful delete on {id}!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error delete sun data");
            return NotFound("Error delete sun data");
        }
    }

    [HttpPost("Post"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<SunTime>> Post(string name)
    {
        try
        {
            var existingCity = await _repository.Cities.FirstOrDefaultAsync(c => c.Name == name);
            if (existingCity != null)
            {
                _logger.LogInformation($"Data for {name} already exists in the database.");
                return Ok(existingCity);
            }

            var weatherData = await _weatherDataProvider.GetCoordinates(name);
            var city = _jsonProcessor.CityProcess(weatherData);

            var sunsetSunrise = await _weatherDataProvider.GetSunTime(city.Latitude, city.Longitude);
            var time = _jsonProcessor.SunTimeProcess(sunsetSunrise);

            var newCity = new City(city.Name, city.Longitude, city.Latitude, city.State, city.Country);
            
            _repository.Cities.Add(newCity);
            await _repository.SaveChangesAsync();
            
            var cityId = newCity.Id;

            var newSunTime = new SunTime(time.SunRiseTime, time.SunSetTime) { CityId = cityId };
            
            _repository.SunTimes!.Add(newSunTime);
            await _repository.SaveChangesAsync();

            return Ok(time);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error posting sun data");
            return NotFound("Error posting sun data");
        }
    }
}