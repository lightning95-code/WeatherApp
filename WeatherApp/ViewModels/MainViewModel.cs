using System;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;
using System.Windows;
using System.ComponentModel;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService;
        private CurrentWeather _currentWeather;
        private string _cityName;
        private string _temperature;
        private string _weatherDescription;
        private string _feelsLike;
        private string _date;
        private string _maxTemperature;
        private string _minTemperature;
        private string _visibility;
        private string _humidity;
        private string _pressure;
        private string _windSpeed;
        private string _weatherIcon;

        private string _cityImageUrl;

        public MainViewModel()
        {
            _weatherService = new WeatherService();
            CityName = "London";
            LoadWeatherDataAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string CityImageUrl
        {
            get => _cityImageUrl;
            set
            {
                _cityImageUrl = value;
                OnPropertyChanged(nameof(CityImageUrl));
            }
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                if (_cityName != value)
                {
                    _cityName = value;
                    OnPropertyChanged(nameof(CityName));
                    LoadWeatherDataAsync();
                }
            }
        }

        public string Temperature
        {
            get => _temperature;
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    OnPropertyChanged(nameof(Temperature));
                }
            }
        }

        public string WeatherDescription
        {
            get => _weatherDescription;
            set
            {
                if (_weatherDescription != value)
                {
                    _weatherDescription = value;
                    OnPropertyChanged(nameof(WeatherDescription));
                }
            }
        }

        public string FeelsLike
        {
            get => _feelsLike;
            set
            {
                if (_feelsLike != value)
                {
                    _feelsLike = value;
                    OnPropertyChanged(nameof(FeelsLike));
                }
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string MaxTemperature
        {
            get => _maxTemperature;
            set
            {
                if (_maxTemperature != value)
                {
                    _maxTemperature = value;
                    OnPropertyChanged(nameof(MaxTemperature));
                }
            }
        }

        public string MinTemperature
        {
            get => _minTemperature;
            set
            {
                if (_minTemperature != value)
                {
                    _minTemperature = value;
                    OnPropertyChanged(nameof(MinTemperature));
                }
            }
        }

        public string Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility != value)
                {
                    _visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }

        public string Humidity
        {
            get => _humidity;
            set
            {
                if (_humidity != value)
                {
                    _humidity = value;
                    OnPropertyChanged(nameof(Humidity));
                }
            }
        }

        public string Pressure
        {
            get => _pressure;
            set
            {
                if (_pressure != value)
                {
                    _pressure = value;
                    OnPropertyChanged(nameof(Pressure));
                }
            }
        }

        public string WindSpeed
        {
            get => _windSpeed;
            set
            {
                if (_windSpeed != value)
                {
                    _windSpeed = value;
                    OnPropertyChanged(nameof(WindSpeed));
                }
            }
        }


        public string WeatherIcon
        {
            get => _weatherIcon;
            set
            {
                if (_weatherIcon != value)
                {
                    _weatherIcon = value;
                    OnPropertyChanged(nameof(WeatherIcon));
                }
            }
        }

        
        public async Task LoadCityImageAsync(string cityName)
        {
            var pixabayService = new PixabayService();
            try
            {
                CityImageUrl = await pixabayService.GetCityImageAsync(cityName);
            }
            catch (Exception ex)
            {
                
                CityImageUrl = "FallbackImagePath.png"; 
                Console.WriteLine($"Error loading image: {ex.Message}");
            }
        }

        public async Task LoadWeatherDataAsync()
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync(CityName);

                if (weatherData != null)
                {
                    LoadCityImageAsync(CityName);
                    _currentWeather = weatherData;

                    if (_currentWeather.TempCelsius != 0)
                        Temperature = $"Current temperature: {_currentWeather.TempCelsius:F1} °C";
                    else
                        Temperature = "Currently unknown";

                    if (_currentWeather.Weather.Count > 0 && !string.IsNullOrEmpty(_currentWeather.Weather[0].Description))
                        WeatherDescription = _currentWeather.Weather[0].Description;
                    else
                        WeatherDescription = "Currently unknown";

                    if (_currentWeather.FeelsLikeCelsius != -273.15 && _currentWeather.FeelsLikeCelsius != 0)
                        FeelsLike = $"Feels like: {_currentWeather.FeelsLikeCelsius:F1} °C";
                    else
                        FeelsLike = $"Feels like: {1 + _currentWeather.TempCelsius:F1} °C";

                    Date = $"({DateTime.Now.ToString("dd.MM.yyyy")})";

                    if (_currentWeather.TempMaxCelsius != -273.15 && _currentWeather.TempMaxCelsius != 0)
                        MaxTemperature = $"Maximum temperature: {_currentWeather.TempMaxCelsius:F1} °C";
                    else
                        MaxTemperature = $"Maximum temperature: { 4 + _currentWeather.TempCelsius:F1} °C";;

                    if (_currentWeather.TempMinCelsius != -273.15 && _currentWeather.TempMinCelsius!=0)
                        MinTemperature = $"Minimum temperature: {_currentWeather.TempMinCelsius:F1} °C";
                    else
                        MinTemperature = $"Minimum temperature: {-4 + _currentWeather.TempCelsius:F1} °C";

                    if (_currentWeather.Visibility != 0)
                        Visibility = $"Visibility: {_currentWeather.Visibility} meters";
                    else
                        Visibility = "Currently unknown";

                    if (_currentWeather.Main.Humidity != 0)
                        Humidity = $"Humidity: {_currentWeather.Main.Humidity} %";
                    else
                        Humidity = "Currently unknown";

                    if (_currentWeather.Main.Pressure != 0)
                        Pressure = $"Pressure: {_currentWeather.Main.Pressure} hPa";
                    else
                        Pressure = "Currently unknown";

                    if (_currentWeather.Wind.Speed != 0)
                        WindSpeed = $"Wind speed: {_currentWeather.Wind.Speed} m/s";
                    else
                        WindSpeed = "Currently unknown";

                    if (_currentWeather.Weather.Count > 0 && !string.IsNullOrEmpty(_currentWeather.Weather[0].Description))
                    {
                      
                        WeatherIcon = $"http://openweathermap.org/img/w/{_currentWeather.Weather[0].Icon}.png"; // URL 
                    }
                    else
                    {
                      
                        WeatherIcon = null; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving data about weather: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
