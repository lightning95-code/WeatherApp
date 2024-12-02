namespace WeatherApp.Models
{
    public class ForecastDetailResponse
    {
        public string Cod { get; set; } // Код відповіді
        public int Message { get; set; }
        public int Cnt { get; set; } // Кількість періодів прогнозу
        public List<DetailForecast> List { get; set; } // Список періодів прогнозів
        public WeatherCity City { get; set; } // Информация о городе
    }
}
