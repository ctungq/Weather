using System.Text.Json;
using Weather.Contracts;

namespace Weather.Api;

public class WeatherMapGateway : IWeatherMapGateway
{
    public WeatherResponse GetWeather(string cityAndCountry)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.openweathermap.org/");

        var response = client.GetAsync($"data/2.5/weather?q={cityAndCountry}&appid=8b7535b42fe1c551f18028f64e8688f7").Result;

        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            WeatherResponse r = JsonSerializer.Deserialize<WeatherResponse>(responseContent);
            return r;
        }
        
        return null;
    }
}
