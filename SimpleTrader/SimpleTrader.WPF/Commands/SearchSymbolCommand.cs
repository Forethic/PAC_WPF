using System;
using System.Windows;
using System.Windows.Input;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : ICommand
    {
        private BuyViewModel _ViewModel;
        private IStockPriceService _StockPriceService;

        public event EventHandler CanExecuteChanged;

        public SearchSymbolCommand(BuyViewModel viewModel, IStockPriceService stockPriceService)
        {
            _ViewModel = viewModel;
            _StockPriceService = stockPriceService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                double stockPrice = await _StockPriceService.GetPrice(_ViewModel.Symbol);
                _ViewModel.StockPrice = stockPrice;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}