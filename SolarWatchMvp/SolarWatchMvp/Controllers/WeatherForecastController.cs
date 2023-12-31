using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

[ApiController]
[Route("Sunset-Sunrise")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherApiContext weatherApiContext)
    : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger = logger;
    private readonly WeatherApiContext _repository = weatherApiContext;

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
            await foreach (var repositoryCity in _repository.Cities)
            {
                
            }
            
            var existingSunTime = await _repository.SunTimes!.FirstOrDefaultAsync(sunTime => sunTime.CityId == existingCity.Id);
            if (existingSunTime == null)
            {
                return Ok(existingCity);
            }
            
            var result = new {
                Name = existingCity.Name,
                SunRiseTime = existingSunTime.SunRiseTime,
                SunSetTime = existingSunTime.SunSetTime
            };

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sun data");
            return NotFound("Error getting sun data");
        }
    }
}