namespace SolarWatchMvp.Controllers;

public interface IWeatherDataProvider
{
    Task<string> GetCoordinates(string cityName);
    Task<string> GetSunTime(double lat, double lon);
}