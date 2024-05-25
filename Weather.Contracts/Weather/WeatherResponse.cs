namespace Weather.Contracts;

public record WeatherResponse(
    int Id,
    int Icon,
    int Description
);