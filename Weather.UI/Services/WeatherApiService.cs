using System.Text.Json;
using Weather.Contracts;

namespace Weather.UI;

public class WeatherApiService : IWeatherApiService
{
    private readonly string _weatherApiApiKey;
    private readonly string _weatherApiBaseUrl;
    private readonly string _weatherApiQuery;

    public WeatherApiService(string weatherApiApiKey, string weatherApiBaseUrl, string weatherApiQuery)
    {
        _weatherApiApiKey = weatherApiApiKey;
        _weatherApiBaseUrl = weatherApiBaseUrl;
        _weatherApiQuery = weatherApiQuery;
    }

    public string GetWeatherDescription(string city, string country)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(_weatherApiBaseUrl)
        };
        client.DefaultRequestHeaders.Add("X-API-KEY", _weatherApiApiKey);

        var response = client.GetAsync(_weatherApiQuery.Replace("{city}", city).Replace("{country}", country)).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(responseContent);
            WeatherResponse r = JsonSerializer.Deserialize<WeatherResponse>(
                responseContent, 
                new JsonSerializerOptions{ PropertyNameCaseInsensitive = true }
                );
            return r.Description;
        }
        
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return "Cannot connect to retrieve data";
        }

        return "Could not retrieve weather data";
    }
}
