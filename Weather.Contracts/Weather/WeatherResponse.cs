namespace Weather.Contracts;

public record WeatherResponse(
    Coord Coord,
    Weather[] Weather,
    Main Main
);