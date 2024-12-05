using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
    }
}
