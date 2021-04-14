using System.Windows;
using System.Windows.Input;

namespace ScreenToGif
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DragDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void BtnExit_Click(object sender, RoutedEventArgs e) => Close();
    }
}