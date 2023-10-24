using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

public interface IJsonProcessor
{
    City CityProcess(string data);
    SunTime SunTimeProcess(string data);
}