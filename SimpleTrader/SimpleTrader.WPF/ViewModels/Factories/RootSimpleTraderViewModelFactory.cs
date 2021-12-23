using System;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RootSimpleTraderViewModelFactory : IRootSimpleTraderViewModelFactory
    {
        private readonly ISimpleTraderViewModelFactory<LoginViewModel> _LoginViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _HomeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _PortfolioViewModelFactory;
        private readonly BuyViewModel _BuyViewModel;

        public RootSimpleTraderViewModelFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory, ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory, BuyViewModel buyViewModel, ISimpleTraderViewModelFactory<LoginViewModel> loginViewModelFactory)
        {
            _HomeViewModelFactory = homeViewModelFactory;
            _PortfolioViewModelFactory = portfolioViewModelFactory;
            _BuyViewModel = buyViewModel;
            _LoginViewModelFactory = loginViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _HomeViewModelFactory.CreateViewModel(),
                ViewType.Portfolio => _PortfolioViewModelFactory.CreateViewModel(),
                ViewType.Buy => _BuyViewModel,
                ViewType.Login => _LoginViewModelFactory.CreateViewModel(),
                _ => throw new ArgumentException("The ViewType does not havea ViewModel.", "viewType"),
            };
        }
    }
}