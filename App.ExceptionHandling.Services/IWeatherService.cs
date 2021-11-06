using App.Domain;
using System.Collections.Generic;

namespace App.ExceptionHandling.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> Get(string cityName);
    }
}
