using Weather.Contracts;

namespace Weather.Api;

public class WeatherService : IWeatherService
{
    public WeatherResponse GetWeather(string CityAndCountry)
    {
        var response = new WeatherResponse(
            new Coord(11, 22),
            [new Contracts.Weather(11, "Clouds", "few clouds", "02d")],
            new Main(21, 1017, 75, 15, 21, null, null),
            "London"
        );

        return response;
    }
}
