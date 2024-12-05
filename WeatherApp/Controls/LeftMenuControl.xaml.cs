using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using WeatherApp.Utilities;
using WeatherApp.Views;

namespace WeatherApp.Controls
{

    public partial class LeftMenuControl : UserControl
    {
        public LeftMenuControl()
        {
            InitializeComponent();
        }


        private async void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            // Запуск анімації скриття
            DoubleAnimation hideAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.5) 
            };

            // Створення асинхронної задачі, яка завершиться по закінченню анімації
            TaskCompletionSource<bool> animationCompletion = new TaskCompletionSource<bool>();

            
            hideAnimation.Completed += (s, a) => animationCompletion.SetResult(true);

         
            this.BeginAnimation(UIElement.OpacityProperty, hideAnimation);

          
            await animationCompletion.Task;
            await Task.Delay(100);
    
            Application.Current.Shutdown();
        }

        private async void HomeButtonClick(object sender, RoutedEventArgs e)
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

        private async void ForecastPage_Click(object sender, RoutedEventArgs e)
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

            ForecastView ForecastView = new ForecastView
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };
            ForecastView.Show();

            // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
            await Task.Delay(100);

            window.Close();
        }
    }
}
