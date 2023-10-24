namespace SolarWatchMvp.Model;

public class City
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string? State { get; init; }
    public string? Country { get; init; }
}