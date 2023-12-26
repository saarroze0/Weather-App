using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weather_App.Services;
using Weather_App.Models;

namespace Weather_App_Tests
{
    [TestClass]
    public class WeatherServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _mockHttpClient;

        [TestInitialize]
        public void SetUp()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
        }

        [TestMethod]
        public async Task GetWeatherAsync_ReturnsWeatherData()
        {
            // Arrange
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{" +
                    "\"main\":{\"temp\":15,\"feels_like\":14}," +
                    "\"name\":\"Test City\"," +
                    "\"weather\":[{\"description\":\"clear sky\"}]" +
                    "}", System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(fakeResponse);

            var weatherService = new WeatherService(_mockHttpClient);

            // Act
            var result = await weatherService.GetWeatherAsync("Test City");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test City", result.City);
            Assert.AreEqual(15, result.Temperature);
            Assert.AreEqual("clear sky", result.Description);
        }
    }
}
