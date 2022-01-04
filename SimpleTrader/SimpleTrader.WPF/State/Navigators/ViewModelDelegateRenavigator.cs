using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.State.Navigators
{
    public class ViewModelDelegateRenavigator<TViewModel> : IRenavigator
        where TViewModel : ViewModelBase
    {
        private readonly INavigator _Navigator;
        private readonly CreateViewModel<TViewModel> _createViewModel;

        public ViewModelDelegateRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModel)
        {
            _createViewModel = createViewModel;
            _Navigator = navigator;
        }

        public void Renavigate()
        {
            _Navigator.CurrentViewModel = _createViewModel();
        }
    }
}