using Weather.Contracts;

namespace Weather.Api;

public interface IWeatherMapGateway
{
    WeatherMapResponse GetWeather(string city, string country);
}
