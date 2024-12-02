namespace WeatherApp.Models
{
    public class CurrentWeather
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public int Visibility { get; set; }
        public long Dt { get; set; } // Date
        public string City_Name { get; set; } 
        public int Cod { get; set; }


        public double TempCelsius => Main.Temp - 273.15;
        public double FeelsLikeCelsius => Main.FeelsLike - 273.15;
        public double TempMinCelsius => Main.TempMin - 273.15;
        public double TempMaxCelsius => Main.TempMax - 273.15;
    }

    public class Coord
    {
        public double Lon { get; set; }  // Долгота
        public double Lat { get; set; }  // Широта
    }

    public class Weather
    {
        public string Main { get; set; }  // Основний опис погоди (наприклад, "Rain")
        public string Description { get; set; }        // Детальний опис погоди (наприклад, "light rain")
        public string Icon { get; set; }  
    }

    public class Main
    {
        public double Temp { get; set; }  // в Кельвінах
        public double FeelsLike { get; set; }  
        public double TempMin { get; set; }  
        public double TempMax { get; set; }  
        public int Pressure { get; set; }  
        public int Humidity { get; set; }  // in %
    }

    public class Wind
    {
        public double Speed { get; set; }  
        public int Deg { get; set; }  // Напрям вітра у градусах
    }


}
