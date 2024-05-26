using System.Security.Permissions;

namespace Weather.Contracts;

public record WeatherMapResponse(
    Weather[]? Weather,
    int cod
);