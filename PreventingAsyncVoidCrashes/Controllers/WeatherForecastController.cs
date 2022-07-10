using Microsoft.AspNetCore.Mvc;
using PreventingAsyncVoidCrashes.Models;
using PreventingAsyncVoidCrashes.Service;
using System.ComponentModel;
using System.Diagnostics;

namespace PreventingAsyncVoidCrashes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecast _weatherForecast = new();

        [HttpGet("ThisWillContinueAfterAnException/{forecastDays}")]
        [Description("This calls an async task which returns an unhandled exception but uses a fire and forget task to do it avoiding a crash, view Debug while running")]
        public IEnumerable<Weather> Get(int forecastDays)
        {
            Task.Run(GoodBackgroundMethod);
            return _weatherForecast.GetWeatherForecast(forecastDays);
        }

        [HttpGet("ThisWillCrashAfterAnException/{forecastDays}")]
        [Description("This calls an async void method which returns an unhandled exception causing a crash, view Debug while running")]
        public IEnumerable<Weather> GetCrashing(int forecastDays)
        {
            BadBackgroundMethod();
            return _weatherForecast.GetWeatherForecast(forecastDays);
        }

        private static async void BadBackgroundMethod()
        {
            await Task.Delay(1000);
            Debug.WriteLine("\nThis exception was from the async void method and has crashed the api");
            throw new Exception();
        }

        private static async Task GoodBackgroundMethod()
        {
            await Task.Delay(1000);
            Debug.WriteLine("\nThis exception was handled in a fire and forget task and has not crashed the api");
            throw new Exception();
        }
    }
}