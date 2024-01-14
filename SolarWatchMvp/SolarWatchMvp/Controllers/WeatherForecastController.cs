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
    [HttpGet("Get"), Authorize]
    public async Task<ActionResult<SunTime>> GetSunTime(string name)
    {
        try
        {
            var existingCity = await weatherApiContext.Cities!.FirstOrDefaultAsync(c => c.Name == name);
            if (existingCity == null)
            {
                logger.LogInformation($"Data for {name} not exists in the database.");
                return Ok(existingCity);
            }
            
            var existingSunTime = await weatherApiContext.SunTimes!.FirstOrDefaultAsync(sunTime => sunTime.CityId == existingCity.Id);
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
            logger.LogError(e, "Error getting sun data");
            return NotFound("Error getting sun data");
        }
    }
}