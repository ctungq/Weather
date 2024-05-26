using Weather.Contracts;

namespace Weather.Api;

public interface IWeatherMapGateway
{
    WeatherResponse GetWeather(string cityAndCountry);
}
