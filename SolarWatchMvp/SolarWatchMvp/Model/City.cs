namespace SolarWatchMvp.Model;

public class City
{
    public string Name { get; init; }
    public CityCoordinate Coordinate { get; init; }
    public string State { get; set; }
    public string Country { get; set; }

    public City(string name, CityCoordinate coordinate, string state, string country)
    {
        Name = name;
        Coordinate = coordinate;
        State = state;
        Country = country;
    }
}