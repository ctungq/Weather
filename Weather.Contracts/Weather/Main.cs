namespace Weather.Contracts;

public record Main(
    decimal? Temp,
    int? Pressure,
    int? Humidity,
    decimal? TempMin,
    decimal? TempMax,
    decimal? SeaLevel,
    decimal? GrndLevel
);