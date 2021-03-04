using Panuon.UI.Silver.Browser.ViewModels;
using System.Windows;

namespace Panuon.UI.Silver.Browser
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Property

        public ShellViewModel ViewModel { get; set; }

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new ShellViewModel();
            DataContext = ViewModel;
        }

        #region Event


        #endregion
    }
}