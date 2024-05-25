
using Microsoft.AspNetCore.Mvc;
using Weather.Contracts;

namespace Weather.Api;

[ApiController]
public class WeatherApiController : ControllerBase
{
    [HttpGet("/weather")]
    public IActionResult GetWeather(string CityAndCountry) 
    {
        var response = new WeatherResponse(
            new Coord(11, 22),
            [new Contracts.Weather(11, "Clouds", "few clouds", "02d")],
            new Main(21, 1017, 75, 15, 21, null, null)
        );
        return Ok(response);
    }
}
