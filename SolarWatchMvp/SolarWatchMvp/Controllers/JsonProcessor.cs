using System.Text.Json;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

public class JsonProcessor : IJsonProcessor
{
    public CityCoordinate CoordinateProcess(string data)
    {
        var json = JsonDocument.Parse(data);

        return new CityCoordinate
        {
            Latitude = json.RootElement[0].GetProperty("lat").GetDouble(),
            Longitude = json.RootElement[0].GetProperty("lon").GetDouble()
        };
    }

    public SunTime SunTimeProcess(string data)
    {
        var json = JsonDocument.Parse(data);
        var results = json.RootElement.GetProperty("results");

        return new SunTime
        {
            SunRiseTime = results.GetProperty("sunrise").ToString(),
            SunSetTime = results.GetProperty("sunset").ToString()
        };
    }
}