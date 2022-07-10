using PreventingAsyncVoidCrashes.Models;

namespace PreventingAsyncVoidCrashes.Service
{
    public class WeatherForecast
    {
        private readonly Dictionary<string, int> _weathers = new()
        {
            { "Cold", 10 },
            { "Cool", 15 },
            { "Warm", 20 },
            { "Hot", 30 }
        };
        
        private int RandomWeatherIndex => new Random().Next(0, _weathers.Count);

        public IEnumerable<Weather> GetWeatherForecast(int days)
        {
            IList<Weather> forecast = new List<Weather>();
            int currentWeatherIndex;

            for (int i = 1; i <= days; i++)
            {
                currentWeatherIndex = RandomWeatherIndex;
                forecast.Add(new Weather()
                {
                    Date = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"),
                    TemperatureC = _weathers.Values.ElementAt(currentWeatherIndex),
                    Summary = _weathers.Keys.ElementAt(currentWeatherIndex)
                });
            }
            return forecast.ToArray();
        }
    }
}
