using System.Windows.Input;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
        private string _Symbol;

        public string Symbol
        {
            get { return _Symbol; }
            set
            {
                _Symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }

        private double _StockPrice = 1.56456;
        public double StockPrice
        {
            get => _StockPrice;
            set
            {
                _StockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
            }
        }

        private int _ShareToBuy = 457;
        public int ShareToBuy
        {
            get => _ShareToBuy;
            set
            {
                _ShareToBuy = value;
                OnPropertyChanged(nameof(ShareToBuy));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice => ShareToBuy * StockPrice;

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService);
        }
    }
}