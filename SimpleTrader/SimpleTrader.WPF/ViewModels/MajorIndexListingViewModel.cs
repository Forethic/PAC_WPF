using System.Threading.Tasks;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private IMajorIndexService _MajorIndexService;

        private MajorIndex _DowJones;
        public MajorIndex DowJones
        {
            get => _DowJones;
            set
            {
                _DowJones = value;
                OnPropertyChanged(nameof(DowJones));
            }
        }

        private MajorIndex _Nasdaq;
        public MajorIndex Nasdaq
        {
            get => _Nasdaq;
            set
            {
                _Nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }

        private MajorIndex _SP500;
        public MajorIndex SP500
        {
            get => _SP500;
            set
            {
                _SP500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }

        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _MajorIndexService = majorIndexService;
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);

            majorIndexViewModel.LoadMajorIndexesAsync();

            return majorIndexViewModel;
        }

        private void LoadMajorIndexesAsync()
        {
            _MajorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith((task) =>
            {
                if (task.Exception != null)
                {
                    DowJones = task.Result;
                }
            });
            _MajorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith((task) =>
            {
                if (task.Exception != null)
                {
                    Nasdaq = task.Result;
                }
            });
            _MajorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith((task) =>
            {

                if (task.Exception != null)
                {
                    SP500 = task.Result;
                }
            });
        }
    }
}