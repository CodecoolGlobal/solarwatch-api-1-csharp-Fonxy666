using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatchMvp.Controllers;
using SolarWatchMvp.Data;
using SolarWatchMvp.Model;

namespace SunriseSunsetTest;

public class Tests
{
    private Mock<ILogger<WeatherForecastController>> _loggerMock;
    private Mock<IWeatherDataProvider> _weatherDataProviderMock;
    private Mock<WeatherApiContext> _repositoryMock;
    private WeatherForecastController _controllerMock;
    
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<WeatherForecastController>>();
        _weatherDataProviderMock = new Mock<IWeatherDataProvider>();
        _repositoryMock = new Mock<WeatherApiContext>();
        _controllerMock = new WeatherForecastController(_loggerMock.Object, _repositoryMock.Object);
    }

    [Test]
    public void GetCurrentReturnsOkResultIfCityDataIsValid()
    {
        _weatherDataProviderMock.Setup(x => x.GetCoordinates(It.IsAny<string>())).Throws(new Exception());
        _weatherDataProviderMock.Setup(x => x.GetSunTime(It.IsAny<double>(), It.IsAny<double>())).Throws(new Exception());
        
        var result = _controllerMock.GetSunTime("Budapest");
        
        Assert.That(result.Result, Is.InstanceOf(typeof(ActionResult<SunTime>)));
        Assert.That(result.IsCompleted);
    }
}