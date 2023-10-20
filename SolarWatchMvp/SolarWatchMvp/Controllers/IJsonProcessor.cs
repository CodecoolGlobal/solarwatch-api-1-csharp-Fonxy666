using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

public interface IJsonProcessor
{
    CityCoordinate CoordinateProcess(string data);
    SunTime SunTimeProcess(string data);
}