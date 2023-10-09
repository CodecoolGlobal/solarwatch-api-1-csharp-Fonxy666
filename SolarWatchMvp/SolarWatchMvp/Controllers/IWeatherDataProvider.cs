namespace SolarWatchMvp.Controllers;

public interface IWeatherDataProvider
{
    public string GetCoordinates(string cityName);
    public string GetSunTime(double lat, double lon);
}