namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory : ISimpleTraderViewModelFactory<HomeViewModel>
    {
        private ISimpleTraderViewModelFactory<MajorIndexListingViewModel> _MajorIndexViewModelFactory;

        public HomeViewModelFactory(ISimpleTraderViewModelFactory<MajorIndexListingViewModel> majorIndexViewModelFactory)
        {
            _MajorIndexViewModelFactory = majorIndexViewModelFactory;
        }

        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(_MajorIndexViewModelFactory.CreateViewModel());
        }
    }
}