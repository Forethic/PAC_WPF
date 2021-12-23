using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IAuthenticator _Authenticator;
        private readonly LoginViewModel _LoginViewModel;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator)
        {
            _Authenticator = authenticator;
            _LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            bool success = await _Authenticator.Login(_LoginViewModel.Username, parameter.ToString());
        }
    }
}