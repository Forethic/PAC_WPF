using System.Threading.Tasks;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexViewModel : ViewModelBase
    {
        private IMajorIndexService _MajorIndexService;

        public MajorIndex DowJones { get; set; }
        public MajorIndex Nasdaq { get; set; }
        public MajorIndex SP500 { get; set; }

        public MajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            _MajorIndexService = majorIndexService;
        }

        public static MajorIndexViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);

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