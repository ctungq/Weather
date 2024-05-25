
using Microsoft.AspNetCore.Mvc;
using Weather.Contracts;

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
    public IActionResult GetWeather(string cityAndCountry) 
    {
        var response = _weatherService.GetWeather(cityAndCountry);
        
        return Ok(response);
    }
}
