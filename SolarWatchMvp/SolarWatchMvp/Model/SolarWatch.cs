using System.ComponentModel.DataAnnotations;

namespace SolarWatchMvp.Model;

public class SunTime
{
    [Key]
    public int Id { get; set; }
    public string SunRiseTime { get; set; }
    public string SunSetTime { get; set; }
}