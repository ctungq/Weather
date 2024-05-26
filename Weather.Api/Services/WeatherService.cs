using Weather.Contracts;

namespace Weather.Api;

public class WeatherService : IWeatherService
{
    private readonly IWeatherMapGateway _weatherMapGateway;

    public WeatherService(IWeatherMapGateway weatherMapGateway)
    {
        _weatherMapGateway = weatherMapGateway;
    }

    public WeatherResponse GetWeather(string city, string country)
    {
        var response = _weatherMapGateway.GetWeather(city, country);
        //Console.WriteLine($"{response.Name}");

        if (response.cod == 400)
            return new WeatherResponse("Bad Request: City and country not found");
            
        if (response.cod == 200 && response.Weather != null && response.Weather.Length > 0)
            return new WeatherResponse(response.Weather[0].Description);

        return new WeatherResponse(null);
    }
}
