using Microsoft.AspNetCore.Authorization;
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
    private readonly WeatherApiContext _repository;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherApiContext weatherApiContext)
    {
        _logger = logger;
        _repository = weatherApiContext;
    }
    
    [HttpGet("Get"), Authorize]
    public async Task<ActionResult<SunTime>> GetSunTime(string name)
    {
        try
        {
            var existingCity = await _repository.Cities!.FirstOrDefaultAsync(c => c.Name == name);
            if (existingCity == null)
            {
                _logger.LogInformation($"Data for {name} not exists in the database.");
                return Ok(existingCity);
            }
            
            var existingSunTime = await _repository.SunTimes!.FirstOrDefaultAsync(sunTime => sunTime.CityId == existingCity.Id);
            if (existingSunTime == null)
            {
                return Ok(existingCity);
            }

            return Ok($"Name: {existingCity.Name} SunRiseTime: {existingSunTime.SunRiseTime}, SunSetTime: {existingSunTime.SunSetTime}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sun data");
            return NotFound("Error getting sun data");
        }
    }
}