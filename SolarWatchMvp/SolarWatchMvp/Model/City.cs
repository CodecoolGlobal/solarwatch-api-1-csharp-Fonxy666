using SolarWatchMvp.Model;

public class City
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int CoordinateId { get; set; } // Updated the property name to CoordinateId

    public string State { get; init; }
    public string Country { get; init; }
}