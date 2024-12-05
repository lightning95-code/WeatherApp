using System;
using System.Windows;
using WeatherApp.Utilities;

namespace WeatherApp.Views
{
    public partial class ForecastView : Window
    {
        public ForecastView()
        {
            InitializeComponent();
            this.DataContext = new ForecastViewModel();

        }

        private async void Go_home_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (window == null)
            {
                MessageBox.Show("Window not found.");
                return;
            }

            // Збереження стану вікна
            WindowStateManager.Width = window.Width;
            WindowStateManager.Height = window.Height;
            WindowStateManager.Top = window.Top;
            WindowStateManager.Left = window.Left;
            WindowStateManager.IsMaximized = window.WindowState == WindowState.Maximized;

            MainWindow mainWin = new MainWindow
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            mainWin.Show();

            // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
            await Task.Delay(100);

            window.Close();
        }
    }
}
