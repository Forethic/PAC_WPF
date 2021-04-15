using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace ScreenToGif.Windows
{
    /// <summary>
    /// Options.xaml 的交互逻辑
    /// </summary>
    public partial class Options : Window
    {
        #region Constructors

        public Options()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(e.Uri.AbsoluteUri);
            }
            catch
            {
                // TODO:
            }
        }

        #endregion
    }
}