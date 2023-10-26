using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatchMvp.Controllers;
using SolarWatchMvp.Data;
using SolarWatchMvp.Model;
using Microsoft.EntityFrameworkCore;

namespace SunriseSunsetTest
{
    public class AdminControllerTests
    {
        private Mock<ILogger<WeatherForecastController>> _loggerMock;
        private Mock<IWeatherDataProvider> _weatherDataProviderMock;
        private WeatherApiContext _context;
        private Mock<IJsonProcessor> _jsonProcessorMock;
        private CrudAdminController _controllerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _weatherDataProviderMock = new Mock<IWeatherDataProvider>();

            var options = new DbContextOptionsBuilder<WeatherApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new WeatherApiContext(options);

            _jsonProcessorMock = new Mock<IJsonProcessor>();
            _controllerMock = new CrudAdminController(_loggerMock.Object, _weatherDataProviderMock.Object,
                _jsonProcessorMock.Object, _context);
        }
        
        [Test]
        public void PostCurrentReturnsOkResultIfCityDataIsValid()
        {
            var result = _controllerMock.Post("Budapest");
            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf(typeof(ActionResult<SunTime>)));
                Assert.That(result.IsCompleted);
            });
        }
        
        [Test]
        public void DeleteCurrentReturnsOkResultIfCityDataIsValid()
        {
            var result = _controllerMock.Delete(1);
            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf(typeof(ActionResult<SunTime>)));
                Assert.That(result.IsCompleted);
            });
        }
        
        [Test]
        public void PutCurrentReturnsOkResultIfCityDataIsValid()
        {
            var result = _controllerMock.Put(1,
                "Budapest",
                19.02,
                19.02,
                "-",
                "HU",
                "19.02",
                "19.02");
            
            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf(typeof(ActionResult<SunTime>)));
                Assert.That(result.IsCompleted);
            });
        }
    }
}