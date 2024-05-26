
using Microsoft.AspNetCore.Mvc;

namespace Weather.Api;

[ApiController]
public class WeatherApiController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherApiController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("/weather")]
    public IActionResult GetWeather(string? city, string? country) 
    {
        if (string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(country)) 
        {
            return BadRequest("City and country are required.");
        }

        if (string.IsNullOrWhiteSpace(city)) 
        {
            return BadRequest("City is required.");
        }

        if (string.IsNullOrWhiteSpace(country)) 
        {
            return BadRequest("Country is required.");
        }

        var response = _weatherService.GetWeather(city, country);
        
        return Ok(response);
    }
}
