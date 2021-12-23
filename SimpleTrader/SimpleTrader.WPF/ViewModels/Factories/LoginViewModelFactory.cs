using SimpleTrader.WPF.State.Authenticators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class LoginViewModelFactory : ISimpleTraderViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _Authenticator;

        public LoginViewModelFactory(IAuthenticator authenticator)
        {
            _Authenticator = authenticator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(_Authenticator);
        }
    }
}