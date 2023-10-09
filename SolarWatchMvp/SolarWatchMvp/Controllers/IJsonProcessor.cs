using SolarWatchMvp.Model;

namespace SolarWatchMvp.Controllers;

public interface IJsonProcessor
{
    CityCoordinate CoordinateProcess(string data);
    SolarWatch SunTimeProcess(string data);
}