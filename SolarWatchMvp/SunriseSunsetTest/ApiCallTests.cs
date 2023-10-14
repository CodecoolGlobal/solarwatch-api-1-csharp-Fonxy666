using Microsoft.Extensions.Logging;
using Moq;
using SolarWatchMvp.Controllers;

namespace SunriseSunsetTest
{
    [TestFixture]
    public class WeatherDataProviderTests
    {
        private ILogger<OpenWeatherMapApi> _logger;
        private IWeatherDataProvider _weatherDataProvider;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<OpenWeatherMapApi>>().Object;
            _weatherDataProvider = new OpenWeatherMapApi(_logger);
        }

        [Test]
        public async Task GetCoordinates_ValidCityName_ReturnsNonEmptyString()
        {
            const string cityName = "New York";
            
            var result = await _weatherDataProvider.GetCoordinates(cityName);
            
            Assert.That(result, Is.Not.Empty, "Result should not be empty.");
        }
        
        [Test]
        public async Task GetSunTime_ValidCityName_ReturnsNonEmptyString()
        {
            const double lat = 12.1;
            const double lon = 12.1;
            
            var result = await _weatherDataProvider.GetSunTime(lat, lon);
            
            Assert.That(result, Is.Not.Empty, "Result should not be empty.");
        }
    }
}