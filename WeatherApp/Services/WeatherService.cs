cousing System;
using System.Net.Http;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly string _apiKey = "76092efc5e8aa6c81386f924b6207574";
        private readonly HttpClient _client;

        public WeatherService()
        {
            _client = new HttpClient();
        }

        public async Task<CurrentWeather> GetWeatherDataAsync(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}&units=metric";
            var response = await _client.GetStringAsync(url);

            var weatherResponse = JsonConvert.DeserializeObject<CurrentWeatherResponse>(response);

            var currentWeather = new CurrentWeather
            {
                Coord = new Coord
                {
                    Lon = weatherResponse.Coord.Lon,
                    Lat = weatherResponse.Coord.Lat
                },
                Weather = new List<Weather>(weatherResponse.Weather),
                Main = weatherResponse.Main,
                Wind = weatherResponse.Wind,
                Visibility = weatherResponse.Visibility,
                Dt = weatherResponse.Dt,
                City_Name = weatherResponse.Name,
                Cod = weatherResponse.Cod
            };

            /*
             // Виведення всіх параметрів у MessageBox без пересчитування
             string weatherDetails = $"City: {currentWeather.City_Name}\n" +
                                     $"Coordinates: ({currentWeather.Coord.Lat}, {currentWeather.Coord.Lon})\n" +
                                     $"Temperature: {currentWeather.Main.Temp}°C\n" +
                                     $"Feels Like: {currentWeather.Main.FeelsLike}°C\n" +
                                     $"Min Temperature: {currentWeather.Main.TempMin}°C\n" +
                                     $"Max Temperature: {currentWeather.Main.TempMax}°C\n" +
                                     $"Pressure: {currentWeather.Main.Pressure} hPa\n" +
                                     $"Humidity: {currentWeather.Main.Humidity}%\n" +
                                     $"Wind Speed: {currentWeather.Wind.Speed} m/s\n" +
                                     $"Wind Direction: {currentWeather.Wind.Deg}°\n" +
                                     $"Visibility: {currentWeather.Visibility} m\n" +
                                                                        $"Date/Time: {DateTimeOffset.FromUnixTimeSeconds(currentWeather.Dt).DateTime}\n" +
                                     $"API Response Code: {currentWeather.Cod}" +
                                     $"Weather Icon: {currentWeather.Weather.FirstOrDefault()?.Icon}"; 

             // Виведення у MessageBox
             MessageBox.Show(weatherDetails, "Weather Information", MessageBoxButton.OK, MessageBoxImage.Information);
            */
            return currentWeather;
        }
    }

    public class CurrentWeatherResponse
    {
        public Coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public int Visibility { get; set; }
        public long Dt { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }

}
