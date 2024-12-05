using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using WeatherApp.ViewModels;

namespace WeatherApp.Controls
{
    /// <summary>
    /// Interaction logic for TopContentControl.xaml
    /// </summary>
    public partial class TopContentControl : UserControl
    {

        private bool IsLanguagePopupOpen = false;
        public TopContentControl()
        {
            InitializeComponent();
            DataContext = new WeatherSearchViewModel();
        }

        // Обробник отримання фокусу на TextBox
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = "";
        }

        // Обробник втрати фокусу на TextBox
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                    searchTextBox.Text = "Search for a city by name.."; // Відновлення тексту при втраті фокусу
                            
            }

            autoCompletePopup.IsOpen = false;
        }

        // Обробник натискання кнопки закриття вікна
        private async void Close_This_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (window == null) return;

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)) // Налаштування анімації зменшення прозорості
            };

            window.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation); // Запуск анімації

            await Task.Delay(500); // Затримка для завершення анімації

            window.Close(); // Закриття вікна
        }

        private void Change_Lang_Button_Click(object sender, RoutedEventArgs e)
        {
            IsLanguagePopupOpen = !IsLanguagePopupOpen;
            LanguagePopup.IsOpen = IsLanguagePopupOpen;
        }

        private void AutoCompleteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBox.SelectedItem is string selectedCity)
            {
                searchTextBox.Text = selectedCity;

               
                AppState.Instance.SelectedCity = selectedCity;

         
                UpdateViewModels(selectedCity);

                
                autoCompletePopup.IsOpen = false;
            }
        }

        private void UpdateViewModels(string selectedCity)
        {
            
            if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.CityName = selectedCity;
            }

            if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.DataContext is ForecastViewModel forecastViewModel)
            {
                forecastViewModel.CityName = selectedCity;
                
            }

        }




    }
}
