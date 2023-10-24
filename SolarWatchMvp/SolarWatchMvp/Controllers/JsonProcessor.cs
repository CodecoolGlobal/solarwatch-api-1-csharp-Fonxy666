using System.Text.Json;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

public class JsonProcessor : IJsonProcessor
{
    public City CityProcess(string data)
    {
        var json = JsonDocument.Parse(data).RootElement[0];
        
        if (json.TryGetProperty("state", out var stateElement))
        {
            return new City
            {
                Name = json.GetProperty("name").ToString(),
                Longitude = json.GetProperty("lon").GetDouble(),
                Latitude = json.GetProperty("lat").GetDouble(),
                Country = json.GetProperty("country").ToString(),
                State = stateElement.ToString()
            };
        }
        else
        {
            return new City
            {
                Name = json.GetProperty("name").ToString(),
                Longitude = json.GetProperty("lon").GetDouble(),
                Latitude = json.GetProperty("lat").GetDouble(),
                Country = json.GetProperty("country").ToString(),
                State = "-"
            };
        }
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