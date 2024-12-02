using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Input;

namespace WeatherApp.Controls
{
    public partial class WindowControl : UserControl
    {
        private bool IsMaximized = false;

        public WindowControl()
        {
            InitializeComponent();
        }

        
        private void WindowBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
               
                var window = Window.GetWindow(this);
                window.DragMove();
            }
        }

        
        private void WindowBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var window = Window.GetWindow(this);
                if (IsMaximized)
                {
                    window.WindowState = WindowState.Normal;
                    window.Width = 1080;
                    window.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    window.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }
    }
}
