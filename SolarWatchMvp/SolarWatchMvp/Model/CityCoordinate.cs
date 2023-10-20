using System.ComponentModel.DataAnnotations;

namespace SolarWatchMvp.Model;

public class CityCoordinate
{
    [Key]
    public int Id { get; set; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}