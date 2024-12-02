using System;
using System.Collections.Generic;

namespace WeatherApp.Models
{
    public class DetailForecast
    {
        public long Dt { get; set; } // date

        public MainWeatherInfo Main { get; set; }
        public int Visibility { get; set; } 
        public string DtTxt { get; set; } // date in txt
        public List<WeatherDescription> Weather { get; set; }

        public WindInfo Wind { get; set; }

        public RainInfo Rain { get; set; }
        public CloudsInfo Clouds { get; set; }
        public double TempCelsius => Main.Temp - 273.15;
        public double FeelsLikeCelsius => Main.FeelsLike - 273.15;
        public double TempMinCelsius => Main.TempMin - 273.15;
        public double TempMaxCelsius => Main.TempMax - 273.15;
    }

    public class MainWeatherInfo
    {
        public double Temp { get; set; } 
        public double FeelsLike { get; set; } 
        public double TempMin { get; set; } 
        public double TempMax { get; set; } 
        public int Pressure { get; set; } //  (гПа)
        public int Humidity { get; set; } // (%)
    }

    public class RainInfo
    {
        public double _3h { get; set; } // 
    }

    public class CloudsInfo
    {
        public int All { get; set; } // %
    }

    public class WindInfo
    {
        public double Speed { get; set; } // (м/с)
        public int Deg { get; set; } // Напрям (градуси)
        public double Gust { get; set; } // Пориви (м/с)
    }

    public class WeatherDescription
    {
        public int Id { get; set; } 
        public string Main { get; set; } 
        public string Description { get; set; }
        public string Icon { get; set; } 
    }

    public class Coordinates
    {
        public double Lat { get; set; } // Широта
        public double Lon { get; set; } // Долгота
    }

  
}
