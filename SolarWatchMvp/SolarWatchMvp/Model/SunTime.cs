namespace SolarWatchMvp.Model;

public class SunTime
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public string? SunRiseTime { get; set; }
    public string? SunSetTime { get; set; }
}