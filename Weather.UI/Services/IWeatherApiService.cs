namespace Weather.UI;

public interface IWeatherApiService
{
    string GetWeatherDescription(string city, string country);
}
