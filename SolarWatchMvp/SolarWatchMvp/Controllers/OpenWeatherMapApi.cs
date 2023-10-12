using System.Net;

namespace SolarWatchMvp.Controllers;

public class OpenWeatherMapApi : IWeatherDataProvider
{
    private readonly ILogger<OpenWeatherMapApi> _logger;
    
    public OpenWeatherMapApi(ILogger<OpenWeatherMapApi> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetCoordinates(string cityName)
    {
        const string apIkey = "dff12a8fd6946ce444e8f792f93eefb4";
        var url = $"https://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=5&appid={apIkey}";

        var client = new HttpClient();

        _logger.LogInformation("Calling WeatherForecast API with url: {url}", url);
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<string> GetSunTime(double lat, double lon)
    {
        var url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}";

        var client = new HttpClient();

        _logger.LogInformation("Calling Sunrise-Sunset API with url: {url}", url);
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}