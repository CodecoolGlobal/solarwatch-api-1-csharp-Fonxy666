using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatchMvp.Controllers;
using SolarWatchMvp.Model;

namespace SunriseSunsetTest;

public class Tests
{
    private Mock<ILogger<WeatherForecastController>> _loggerMock;
    private Mock<IWeatherDataProvider> _weatherDataProviderMock;
    private Mock<IJsonProcessor> _jsonProcessorMock;
    private WeatherForecastController _controller;
    
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<WeatherForecastController>>();
        _weatherDataProviderMock = new Mock<IWeatherDataProvider>();
        _jsonProcessorMock = new Mock<IJsonProcessor>();
        _controller = new WeatherForecastController(_loggerMock.Object, _weatherDataProviderMock.Object, _jsonProcessorMock.Object);
    }

    [Test]
    public void GetCurrentReturnsOkResultIfCityDataIsValid()
    {
        _weatherDataProviderMock.Setup(x => x.GetCoordinates(It.IsAny<string>())).Throws(new Exception());
        _weatherDataProviderMock.Setup(x => x.GetSunTime(It.IsAny<double>(), It.IsAny<double>())).Throws(new Exception());
        
        var result = _controller.GetSunTime("Budapest");
        
        Assert.That(result.Result, Is.InstanceOf(typeof(ActionResult<SolarWatch>)));
    }
}