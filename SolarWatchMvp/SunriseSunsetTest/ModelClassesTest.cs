using SolarWatchMvp.Model;

namespace SunriseSunsetTest
{
    [TestFixture]
    public class CityCoordinateTests
    {
        [Test]
        public void CityCoordinate_WithValidValues_SetValuesCorrectly()
        {
            const double expectedLatitude = 37.7749;
            const double expectedLongitude = -122.4194;

            var cityCoordinate = new CityCoordinate
            {
                Latitude = expectedLatitude,
                Longitude = expectedLongitude
            };
            Assert.Multiple(() =>
            {
                Assert.That(cityCoordinate.Latitude, Is.EqualTo(expectedLatitude), "Latitude should be set to the provided value.");
                Assert.That(cityCoordinate.Longitude, Is.EqualTo(expectedLongitude), "Longitude should be set to the provided value.");
            });
        }
    }
    
    [TestFixture]
    public class SolarWatchTest
    {
        [Test]
        public void CityCoordinate_WithValidValues_SetValuesCorrectly()
        {
            const string expectedSunRiseDate = "2000-01-01";
            const string expectedSunSetDate = "2000-01-01";

            var solarWatch = new SolarWatch
            {
                SunRiseDate = expectedSunRiseDate,
                SunSetDate = expectedSunSetDate
            };
            Assert.Multiple(() =>
            {
                Assert.That(solarWatch.SunRiseDate, Is.EqualTo(expectedSunRiseDate), "Sun rise date should be set to the provided value.");
                Assert.That(solarWatch.SunSetDate, Is.EqualTo(expectedSunSetDate), "Sun set date should be set to the provided value.");
            });
        }
    }
}