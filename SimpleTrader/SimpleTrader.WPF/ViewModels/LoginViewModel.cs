using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _Username;
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand LoginCommand { get; set; }


        public LoginViewModel(IAuthenticator authenticator)
        {
            LoginCommand = new LoginCommand(this, authenticator);
        }
    }
}