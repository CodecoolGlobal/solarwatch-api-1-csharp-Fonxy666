using System.Text.Json;
using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

public class JsonProcessor : IJsonProcessor
{
    public City CityProcess(string data)
    {
        var json = JsonDocument.Parse(data).RootElement[0];
        Console.WriteLine(json);
        
        if (json.TryGetProperty("state", out var stateElement))
        {
            return new City(json.GetProperty("name").ToString(),
                json.GetProperty("lon").GetDouble(),
                json.GetProperty("lat").GetDouble(),
                stateElement.ToString(),
                json.GetProperty("country").ToString());
        }
        else
        {
            return new City(json.GetProperty("name").ToString(),
                json.GetProperty("lon").GetDouble(),
                json.GetProperty("lat").GetDouble(),
                "-",
                json.GetProperty("country").ToString()
                );
        }
    }

    public SunTime SunTimeProcess(string data)
    {
        var json = JsonDocument.Parse(data);
        var results = json.RootElement.GetProperty("results");

        return new SunTime(results.GetProperty("sunrise").ToString(), results.GetProperty("sunset").ToString());
    }
}