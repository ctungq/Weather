using Weather.Contracts;

namespace Weather.Api;

public class WeatherService : IWeatherService
{
    private readonly IWeatherMapGateway _weatherMapGateway;

    public WeatherService(IWeatherMapGateway weatherMapGateway)
    {
        _weatherMapGateway = weatherMapGateway;
    }

    public WeatherResponse GetWeather(string cityAndCountry)
    {
        var response = _weatherMapGateway.GetWeather(cityAndCountry);

        return response;
    }
}
