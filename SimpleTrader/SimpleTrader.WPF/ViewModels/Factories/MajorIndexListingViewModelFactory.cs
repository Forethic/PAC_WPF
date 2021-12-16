using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class MajorIndexListingViewModelFactory : ISimpleTraderViewModelFactory<MajorIndexListingViewModel>
    {
        private IMajorIndexService _MajorIndexService;

        public MajorIndexListingViewModelFactory(IMajorIndexService majorIndexService)
        {
            _MajorIndexService = majorIndexService;
        }

        public MajorIndexListingViewModel CreateViewModel()
        {
            return MajorIndexListingViewModel.LoadMajorIndexViewModel(_MajorIndexService);
        }
    }
}