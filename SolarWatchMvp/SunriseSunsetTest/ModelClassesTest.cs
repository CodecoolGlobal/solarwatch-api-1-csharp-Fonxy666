using SolarWatchMvp.Model;

namespace SunriseSunsetTest;

[TestFixture]
public class CityCoordinateTests
{
    [Test]
    public void CityCoordinate_WithValidValues_SetValuesCorrectly()
    {
        const int expectedId = 1;
        const string expectedName = "Budapest";
        const double expectedLatitude = 37.7749;
        const double expectedLongitude = -122.4194;
        const string expectedState = "-";
        const string expectedCountry = "HU";

        var cityCoordinate = new City
        {
            Id = expectedId,
            Name = expectedName,
            Latitude = expectedLatitude,
            Longitude = expectedLongitude,
            State = expectedState,
            Country = expectedCountry
        };
        Assert.Multiple(() =>
        {
            Assert.That(cityCoordinate.Id, Is.EqualTo(expectedId), "Id should be set to the provided value.");
            Assert.That(cityCoordinate.Name, Is.EqualTo(expectedName), "Name should be set to the provided value.");
            Assert.That(cityCoordinate.Latitude, Is.EqualTo(expectedLatitude), "Latitude should be set to the provided value.");
            Assert.That(cityCoordinate.Longitude, Is.EqualTo(expectedLongitude), "Longitude should be set to the provided value.");
            Assert.That(cityCoordinate.Latitude, Is.EqualTo(expectedLatitude), "State should be set to the provided value.");
            Assert.That(cityCoordinate.Latitude, Is.EqualTo(expectedLatitude), "Country should be set to the provided value.");
        });
    }
}

[TestFixture]
public class SolarWatchTest
{
    [Test]
    public void CityCoordinate_WithValidValues_SetValuesCorrectly()
    {
        const int expectedId = 1;
        const int expectedCityId = 1;
        const string expectedSunRiseDate = "2000-01-01";
        const string expectedSunSetDate = "2000-01-01";

        var solarWatch = new SunTime
        {
            Id = expectedId,
            CityId = expectedCityId,
            SunRiseTime = expectedSunRiseDate,
            SunSetTime = expectedSunSetDate
        };
        Assert.Multiple(() =>
        {
            Assert.That(solarWatch.Id, Is.EqualTo(expectedId), "Id should be set to the provided value.");
            Assert.That(solarWatch.CityId, Is.EqualTo(expectedCityId), "City Id should be set to the provided value.");
            Assert.That(solarWatch.SunRiseTime, Is.EqualTo(expectedSunRiseDate), "Sun rise date should be set to the provided value.");
            Assert.That(solarWatch.SunSetTime, Is.EqualTo(expectedSunSetDate), "Sun set date should be set to the provided value.");
        });
    }
}