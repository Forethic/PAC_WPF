using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTrader.WPF.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

        public static readonly DependencyProperty LoginCommandProperty =
            DependencyProperty.Register("LoginCommand",
                                        typeof(ICommand),
                                        typeof(LoginView),
                                        new PropertyMetadata(null));

        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginCommand != null)
            {
                LoginCommand.Execute(pbPassword.Password);
            }
        }
    }
}