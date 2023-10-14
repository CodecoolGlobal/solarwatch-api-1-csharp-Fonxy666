using SolarWatchMvp.Controllers;

namespace SunriseSunsetTest
{
    [TestFixture]
    public class JsonProcessorTests
    {
        private JsonProcessor jsonProcessor;

        [SetUp]
        public void Setup()
        {
            jsonProcessor = new JsonProcessor();
        }

        [Test]
        public void CoordinateProcess_ValidData_ReturnsCityCoordinate()
        {
            const string data = "[{\"lat\": 40.7128, \"lon\": -74.0060}]";
            
            var result = jsonProcessor.CoordinateProcess(data);
            Assert.Multiple(() =>
            {
                Assert.That(result.Latitude, Is.EqualTo(40.7128));
                Assert.That(result.Longitude, Is.EqualTo(-74.0060));
            });
        }

        [Test]
        public void SunTimeProcess_ValidData_ReturnsSolarWatch()
        {
            // Arrange
            string data = "{\"results\":{\"sunrise\":\"2023-10-14T06:45:00+00:00\",\"sunset\":\"2023-10-14T18:20:00+00:00\"}}";

            // Act
            var result = jsonProcessor.SunTimeProcess(data);

            // Assert
            Assert.AreEqual("2023-10-14T06:45:00+00:00", result.SunRiseDate);
            Assert.AreEqual("2023-10-14T18:20:00+00:00", result.SunSetDate);
        }
    }
}