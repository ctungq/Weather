namespace Weather.Contracts;

public record WeatherRequest(
    string City,
    string Country
);