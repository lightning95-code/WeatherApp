using LiveCharts.Wpf;
using LiveCharts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Models;
using WeatherApp.Services;
using System.Windows;

public class ForecastViewModel : INotifyPropertyChanged
{
    private readonly ForecastService _forecastService;
    private ObservableCollection<DetailForecast> _forecastList;
    private ObservableCollection<double> _temperatureData;
    private bool _isLoading;
    private string _cityName;
    private string _errorMessage;
    private string _weatherIcon;

    private DetailForecast _currentForecast;  

    // LiveCharts properties for graph
    private SeriesCollection _seriesCollection;
    private Axis _xAxis;
    private Axis _yAxis;


    private double _humidity;
    private double _pressure;
    private double _windSpeed;
    private double _cloudness;

    public event PropertyChangedEventHandler PropertyChanged;

    public ForecastViewModel()
    {
        _forecastService = new ForecastService();
        _forecastList = new ObservableCollection<DetailForecast>();
        _temperatureData = new ObservableCollection<double>();
        _weatherIcon = string.Empty;

        CityName = AppState.Instance.SelectedCity;
        GetWeatherForecastAsync();
    }


    public DetailForecast CurrentForecast
    {
        get => _currentForecast;
        set
        {
            _currentForecast = value;
            OnPropertyChanged(nameof(CurrentForecast));
            UpdateWeatherDetails();
        }
    }

    public ObservableCollection<DetailForecast> ForecastList
    {
        get { return _forecastList; }
        set
        {
            _forecastList = value;
            OnPropertyChanged(nameof(ForecastList));
        }
    }

    public ObservableCollection<double> TemperatureData
    {
        get { return _temperatureData; }
        set
        {
            _temperatureData = value;
            OnPropertyChanged(nameof(TemperatureData));
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
                GetWeatherForecastAsync();
            }
        }
    }

    public bool IsLoading
    {
        get { return _isLoading; }
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public string ErrorMessage
    {
        get { return _errorMessage; }
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public string WeatherIcon
    {
        get { return _weatherIcon; }
        set
        {
            _weatherIcon = value;
            OnPropertyChanged(nameof(WeatherIcon));
        }
    }

    public SeriesCollection SeriesCollection
    {
        get => _seriesCollection;
        private set
        {
            _seriesCollection = value;
            OnPropertyChanged(nameof(SeriesCollection));
        }
    }

    public Axis XAxis
    {
        get => _xAxis;
        private set
        {
            _xAxis = value;
            OnPropertyChanged(nameof(XAxis));
        }
    }

    public Axis YAxis
    {
        get => _yAxis;
        private set
        {
            _yAxis = value;
            OnPropertyChanged(nameof(YAxis));
        }
    }

    public double Humidity
    {
        get => _humidity;
        set
        {
            _humidity = value;
            OnPropertyChanged(nameof(Humidity));
        }
    }

    public double Pressure
    {
        get => _pressure;
        set
        {
            _pressure = value;
            OnPropertyChanged(nameof(Pressure));
        }
    }

    public double WindSpeed
    {
        get => _windSpeed;
        set
        {
            _windSpeed = value;
            OnPropertyChanged(nameof(WindSpeed));
        }
    }

    public double Cloudness
    {
        get => _cloudness;
        set
        {
            _cloudness = value;
            OnPropertyChanged(nameof(Cloudness));
        }
    }

    private void UpdateWeatherDetails()
    {
        if (CurrentForecast != null)
        {
            Humidity = CurrentForecast.Main.Humidity;
            Pressure = CurrentForecast.Main.Pressure;
            WindSpeed = CurrentForecast.Wind.Speed;
            Cloudness = CurrentForecast.Clouds.All;
        }
    }

    public async Task GetWeatherForecastAsync()
    {
        if (string.IsNullOrEmpty(CityName))
        {
            ErrorMessage = "City name cannot be empty!";
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var forecast = await _forecastService.GetForecastAsync(CityName);

            if (forecast != null && forecast.List != null)
            {
                ForecastList.Clear();
                TemperatureData.Clear();
                var dates = new ObservableCollection<string>();

                var firstForecast = forecast.List.FirstOrDefault();
                if (firstForecast != null)
                {
                    CurrentForecast = firstForecast;
                }

                foreach (var detail in forecast.List)
                {
                    ForecastList.Add(detail);
                    TemperatureData.Add(detail.TempCelsius);
                    dates.Add(DateTimeOffset.FromUnixTimeSeconds(detail.Dt).DateTime.ToString("yyyy-MM-dd"));
                }

                // LiveCharts: Prepare data for temperature graph
                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Temperature",
                    Values = new ChartValues<double>(TemperatureData)
                }
            };

                // Set up axes for the chart
                XAxis = new Axis
                {
                    Title = "Date",
                    Labels = dates.ToArray()
                };

                YAxis = new Axis
                {
                    Title = "Temperature (°C)",
                    LabelFormatter = value => value.ToString("N0") + " °C"
                };

                // Set the WeatherIcon based on the current forecast
                if (CurrentForecast?.Weather?.FirstOrDefault() != null)
                {
                    WeatherIcon = $"http://openweathermap.org/img/wn/{CurrentForecast.Weather[0].Icon}.png";
                }
                UpdateWeatherDetails();
            }
            else
            {
                ErrorMessage = "No data available.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }


    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
