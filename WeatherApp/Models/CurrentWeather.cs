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


        public double TempCelsius
        {
            get
            {
                double temp = Main.Temp;
                if (temp > 50 || temp < -50)
                {
                    
                    return temp - 273.15;
                }
                return temp - 0; 
            }
        }

        public double FeelsLikeCelsius
        {
            get
            {
                double feelsLike = Main.FeelsLike;
                if (feelsLike > 50 || feelsLike < -50)
                {
                    
                    return feelsLike - 273.15; 
                }
                return feelsLike - 0; 
            }
        }

        public double TempMinCelsius
        {
            get
            {
                double tempMin = Main.TempMin ;
                if (tempMin > 50 || tempMin < -50)
                {
                    
                    return tempMin - 273.15; 
                }
                return tempMin - 0; 
            }
        }

        public double TempMaxCelsius
        {
            get
            {
                double tempMax = Main.TempMax;
                if (tempMax > 50 || tempMax < -50)
                {
                  
                    return tempMax - 273.15; 
                }
                return tempMax - 0; 
            }
        }

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
