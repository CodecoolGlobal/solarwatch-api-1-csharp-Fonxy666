using Microsoft.AspNetCore.Mvc;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

[ApiController]
[Route("Sunset-Sunrise")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherDataProvider _weatherDataProvider;
    private readonly IJsonProcessor _jsonProcessor;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherDataProvider weatherDataProvider, IJsonProcessor jsonProcessor)
    {
        _logger = logger;
        _weatherDataProvider = weatherDataProvider;
        _jsonProcessor = jsonProcessor;
    }
    
    [HttpGet("Get")]
    public async Task<ActionResult<SunTime>> GetSunTime(string name)
    {
        try
        {
            var weatherData = await _weatherDataProvider.GetCoordinates(name);
            var coordinates = _jsonProcessor.CoordinateProcess(weatherData);
            
            var sunsetSunrise = await _weatherDataProvider.GetSunTime(coordinates.Latitude, coordinates.Longitude);
            var time = _jsonProcessor.SunTimeProcess(sunsetSunrise);
            
            return Ok(time);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sun data");
            return NotFound("Error getting sun data");
        }
    }
}