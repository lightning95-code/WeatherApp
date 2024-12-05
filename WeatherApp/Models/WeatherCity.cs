namespace WeatherApp.Models
{
    public class WeatherCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
