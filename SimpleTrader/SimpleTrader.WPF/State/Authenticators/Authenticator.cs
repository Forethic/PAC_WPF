using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.WPF.Models;
using System;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.State.Authenticators
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        private IAuthenticationService _authenticationService;

        private Account _CurrentAccount;
        public Account CurrentAccount
        {
            get => _CurrentAccount;
            private set
            {
                _CurrentAccount = value;
                OnPropertyChanged(nameof(CurrentAccount));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
        public bool IsLoggedIn => CurrentAccount != null;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<bool> Login(string username, string password)
        {
            bool success = true;
            try
            {
                CurrentAccount = await _authenticationService.Login(username, password);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public void Logout()
        {
            CurrentAccount = null;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}