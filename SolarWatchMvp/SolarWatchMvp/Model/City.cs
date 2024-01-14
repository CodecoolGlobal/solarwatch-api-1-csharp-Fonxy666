namespace SolarWatchMvp.Model;

public class City(string? name, double longitude, double latitude, string? state, string? country)
{
    public int Id { get; init; }
    public string? Name { get; private set; } = name;
    public double Longitude { get; private set; } = longitude;
    public double Latitude { get; private set; } = latitude;
    public string? State { get; private set; } = state;
    public string? Country { get; private set; } = country;

    public void ChangeCityData(string name, double longitude, double latitude, string state, string county)
    {
        Name = name;
        Longitude = longitude;
        Latitude = latitude;
        State = state;
        Country = county;
    }
}