

namespace SolarWatchMvp.Controllers;

public class OpenWeatherMapApi : IWeatherDataProvider
{
    private readonly ILogger<OpenWeatherMapApi> _logger;
    private readonly IConfiguration _configuration;
    
    public OpenWeatherMapApi(ILogger<OpenWeatherMapApi> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<string> GetCoordinates(string cityName)
    {
        var apiKey = _configuration["ServiceApiKey"];
        Console.WriteLine(apiKey);
        var url = $"https://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=5&appid={apiKey}";

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