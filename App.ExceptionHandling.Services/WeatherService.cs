using App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.ExceptionHandling.Services
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static string[] CitiesData = { "Troy", "Novi", "Canton" };

        public IEnumerable<WeatherForecast> Get(string city)
        {
            if(!CitiesData.Contains(city))
            {
                throw new DomainValidationException($"no weather data available for {city}");
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
