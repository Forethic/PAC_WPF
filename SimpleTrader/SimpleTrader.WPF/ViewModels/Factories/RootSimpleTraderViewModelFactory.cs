using System;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RootSimpleTraderViewModelFactory : IRootSimpleTraderViewModelFactory
    {
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _HomeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _PortfolioViewModelFactory;
        private readonly BuyViewModel _BuyViewModel;

        public RootSimpleTraderViewModelFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory, ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory, BuyViewModel buyViewModel)
        {
            _HomeViewModelFactory = homeViewModelFactory;
            _PortfolioViewModelFactory = portfolioViewModelFactory;
            _BuyViewModel = buyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _HomeViewModelFactory.CreateViewModel(),
                ViewType.Portfolio => _PortfolioViewModelFactory.CreateViewModel(),
                ViewType.Buy => _BuyViewModel,
                _ => throw new ArgumentException("The ViewType does not havea ViewModel.", "viewType"),
            };
        }
    }
}