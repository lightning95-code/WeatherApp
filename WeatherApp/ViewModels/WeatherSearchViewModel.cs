using System.ComponentModel;

public class WeatherSearchViewModel : INotifyPropertyChanged
{
    private string _searchText = "Search for a city by name..";
    private List<string> _filteredCities;
    private bool _isPopupOpen;

    private readonly CityApiService _cityApiService;

    public WeatherSearchViewModel()
    {
        _cityApiService = new CityApiService();
        _filteredCities = new List<string>();
        _isPopupOpen = false;
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterCities();
            }
        }
    }

    public List<string> FilteredCities
    {
        get => _filteredCities;
        set
        {
            _filteredCities = value;
            OnPropertyChanged(nameof(FilteredCities));
        }
    }

    public bool IsPopupOpen
    {
        get => _isPopupOpen;
        set
        {
            _isPopupOpen = value;
            OnPropertyChanged(nameof(IsPopupOpen));
        }
    }

    private async void FilterCities()
    {
        if (!string.IsNullOrWhiteSpace(SearchText) && SearchText != "Search for a city by name..")
        {
            var cities = await _cityApiService.GetCitiesAsync(SearchText);
            FilteredCities = cities;

            IsPopupOpen = FilteredCities.Count > 0;
        }
        else
        {
            FilteredCities.Clear();
            IsPopupOpen = false;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
