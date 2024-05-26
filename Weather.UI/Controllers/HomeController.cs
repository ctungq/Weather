using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Weather.UI.Models;

namespace Weather.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherApiService _weatherApiService;

    public HomeController(ILogger<HomeController> logger, IWeatherApiService weatherApiService)
    {
        _logger = logger;
        _weatherApiService = weatherApiService;
    }

    public IActionResult Index(string city, string country, string submit)
    {
        ViewData["CurrentWeatherMessage"] = "";
        ViewData["Errors"] = "";

        string message = "";

        //simple validation
        if (!string.IsNullOrEmpty(submit))
        {
            if (string.IsNullOrEmpty(city))
            {
                message += "City must have a value. ";
            }

            if (string.IsNullOrEmpty(country))
            {
                message += "Country must have a value. ";
            }

            if (!string.IsNullOrEmpty(message))
            {
                ViewData["Errors"] = message;
                return View();
            }

            //get weather data from api
            var response = _weatherApiService.GetWeatherDescription(city, country);
            ViewData["CurrentWeatherMessage"] = $"Current weather in {city}, {country} is {response}.";
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
