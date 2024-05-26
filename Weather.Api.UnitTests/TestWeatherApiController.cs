using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Weather.Contracts;
using Xunit;

namespace Weather.Api.UnitTests;

public class TestWeatherApiController
{
    [Fact]
    public void Get_OnSuccess_ReturnDescription()
    {
        //Arrange
        var mockWeatherMapGateway = new Mock<IWeatherMapGateway>();
        mockWeatherMapGateway.Setup(gateway => gateway.GetWeather(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new Contracts.WeatherMapResponse([new Contracts.Weather("cloudy")] , 200));
        var mockWeatherService = new WeatherService(mockWeatherMapGateway.Object);// new Mock<IWeatherService>().Object;
        var weatherApiController  = new WeatherApiController(mockWeatherService);

        //Act
        var result = (OkObjectResult)weatherApiController.GetWeather("London", "uk");

        //Assert
        result.StatusCode.Should().Be(200);
        WeatherResponse wr = (WeatherResponse)result.Value;
        wr.Description.Should().Be("cloudy");
    }

    [Fact]
    public void Get_OnNotFound_ReturnBadRequestDescription()
    {
        //Arrange
        var mockWeatherMapGateway = new Mock<IWeatherMapGateway>();
        mockWeatherMapGateway.Setup(gateway => gateway.GetWeather(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new Contracts.WeatherMapResponse(null, 400));
        var mockWeatherService = new WeatherService(mockWeatherMapGateway.Object);
        var weatherApiController  = new WeatherApiController(mockWeatherService);

        //Act
        var result = (OkObjectResult)weatherApiController.GetWeather("London", "uk");

        //Assert
        mockWeatherMapGateway.Verify(gateway => gateway.GetWeather(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
        result.StatusCode.Should().Be(200);
        WeatherResponse wr = (WeatherResponse)result.Value;
        wr.Description.Should().StartWith("Bad Request");
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "abc")]
    [InlineData("abc", null)]
    public void Get_OnMissingCityOrCountry_ReturnBadRequest(string? city, string? country)
    {
        //Arrange
        var mockWeatherService = new Mock<IWeatherService>().Object;
        var weatherApiController  = new WeatherApiController(mockWeatherService);

        //Act
        var result = (BadRequestObjectResult)weatherApiController.GetWeather(city, country);

        //Assert
        result.StatusCode.Should().Be(400);
    }
}