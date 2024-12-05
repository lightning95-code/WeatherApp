namespace WeatherApp.Models
{
    public class DetailForecast
    {
        public long Dt { get; set; }
        public MainWeatherInfo Main { get; set; }
        public int Visibility { get; set; }
        
        public List<WeatherDescription> Weather { get; set; }
        public WindInfo Wind { get; set; }
        public RainInfo Rain { get; set; }
        public CloudsInfo Clouds { get; set; }

        public double TempCelsius => Main.Temp - 273.15;
        public double FeelsLikeCelsius => Main.FeelsLike - 273.15;
        public double TempMinCelsius => Main.TempMin - 273.15;
        public double TempMaxCelsius => Main.TempMax - 273.15;

        public List<TemperatureRecord> TemperatureHistory { get; set; } = new List<TemperatureRecord>();

        public void AddTemperatureRecord(DateTime time, double temperature)
        {
            TemperatureHistory.Add(new TemperatureRecord
            {
                Time = time,
                Temperature = temperature
            });
        }
    }

    public class Forecast
    {
        public List<DetailForecast> List { get; set; }
    }
    public class MainWeatherInfo
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class RainInfo
    {
        public double _3h { get; set; }
    }

    public class CloudsInfo
    {
        public int All { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }
    }

    public class WeatherDescription
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class TemperatureRecord
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
    }
}
