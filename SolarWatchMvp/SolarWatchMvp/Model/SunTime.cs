namespace SolarWatchMvp.Model;

public class SunTime(string? sunRiseTime, string? sunSetTime)
{
    public int Id { get; init; }
    public int CityId { get; init; }
    public string? SunRiseTime { get; private set; } = sunRiseTime;
    public string? SunSetTime { get; private set; } = sunSetTime;

    public void ChangeSunTimeData(string? sunRiseTime, string? sunSetTime)
    {
        SunRiseTime = sunRiseTime;
        SunSetTime = sunSetTime;
    }
}