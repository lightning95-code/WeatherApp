using Newtonsoft.Json;
using System.DirectoryServices.ActiveDirectory;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class ForecastService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string _apiKey = "76092efc5e8aa6c81386f924b6207574";
        private const string _baseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        public async Task<Forecast> GetForecastAsync(string cityName)
        {
            var url = $"{_baseUrl}?q={cityName}&appid={_apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Десериалізація в модель Forecast
                var forecastResponse = JsonConvert.DeserializeObject<Forecast>(jsonResponse);

                // Збереження температури для кожного періоду
                foreach (var forecast in forecastResponse.List)
                {
                    // Конвертація дати
                    DateTime time = DateTimeOffset.FromUnixTimeSeconds(forecast.Dt).DateTime;
                    forecast.AddTemperatureRecord(time, forecast.TempCelsius);
                }

                return forecastResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error when calling OpenWeather API: {ex.Message}");
                return null;
            }
        }
    }
}
