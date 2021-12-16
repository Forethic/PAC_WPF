using System;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class SimpleTraderViewModelAbstractorFactory : ISimpleTraderViewModelAbstractFactory
    {
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _HomeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _PortfolioViewModelFactory;

        public SimpleTraderViewModelAbstractorFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory, ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory)
        {
            _HomeViewModelFactory = homeViewModelFactory;
            _PortfolioViewModelFactory = portfolioViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _HomeViewModelFactory.CreateViewModel();
                case ViewType.Portfolio:
                    return _PortfolioViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The ViewType does not havea ViewModel.", "viewType");
            }
        }
    }
}