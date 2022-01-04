using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.State.Navigators
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator
        where TViewModel : ViewModelBase
    {
        private readonly INavigator _Navigator;
        private readonly ISimpleTraderViewModelFactory<TViewModel> _ViewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator, ISimpleTraderViewModelFactory<TViewModel> viewModelFactory)
        {
            _ViewModelFactory = viewModelFactory;
            _Navigator = navigator;
        }

        public void Renavigate()
        {
            _Navigator.CurrentViewModel = _ViewModelFactory.CreateViewModel();
        }
    }
}