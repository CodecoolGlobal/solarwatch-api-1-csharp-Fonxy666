using SolarWatchMvp.Model;

public class City
{
    public int Id { get; init; }
    public string Name { get; init; }
    public CityCoordinate Coordinate { get; set; }
    public string State { get; init; }
    public string Country { get; init; }

    public City(string name, string state, string country)
    {
        Name = name;
        State = state;
        Country = country;
    }
}