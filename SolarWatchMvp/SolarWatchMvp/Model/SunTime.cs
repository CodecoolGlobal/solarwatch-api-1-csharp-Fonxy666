namespace SolarWatchMvp.Model;

public class SunTime
{
    public int Id { get; init; }
    public int CityId { get; init; }
    public string? SunRiseTime { get; private set; }
    public string? SunSetTime { get; private set; }

    public SunTime(string? sunRiseTime, string? sunSetTime)
    {
        SunRiseTime = sunRiseTime;
        SunSetTime = sunSetTime;
    }
    
    public void ChangeSunTimeData(string? sunRiseTime, string? sunSetTime)
    {
        SunRiseTime = sunRiseTime;
        SunSetTime = sunSetTime;
    }
}