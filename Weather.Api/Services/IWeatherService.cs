﻿using Weather.Contracts;

namespace Weather.Api;

public interface IWeatherService
{
    WeatherResponse GetWeather(string CityAndCountry);
}
