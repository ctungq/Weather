using System.Text.Json;
using Weather.Contracts;

namespace Weather.Api;

public class WeatherMapGateway : IWeatherMapGateway
{
    public WeatherMapResponse GetWeather(string city, string country)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.openweathermap.org/");

        var response = client.GetAsync($"data/2.5/weather?q={city},{country}&appid=8b7535b42fe1c551f18028f64e8688f7").Result;
        Console.WriteLine(response.StatusCode);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseContent);
            WeatherMapResponse r = JsonSerializer.Deserialize<WeatherMapResponse>(
                responseContent, 
                new JsonSerializerOptions{ PropertyNameCaseInsensitive = true }
                );
            return r;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new WeatherMapResponse(null, 400);
        }
        
        return null;
    }
}
