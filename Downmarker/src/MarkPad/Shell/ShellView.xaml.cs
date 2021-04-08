using System.Windows;
using System.Windows.Input;

namespace MarkPad.Shell
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void DragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton != MouseButtonState.Pressed && e.MiddleButton != MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonMaxRestoreOnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                maxRestore.Content = "1";
                WindowState = WindowState.Normal;
            }
            else
            {
                maxRestore.Content = "2";
                WindowState = WindowState.Maximized;
            }
        }

        private void ButtonMinimiseOnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}