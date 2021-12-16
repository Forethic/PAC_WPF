using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : ICommand
    {
        private BuyViewModel _BuyViewModel;
        private IBuyStockService _BuyStockService;

        public event EventHandler CanExecuteChanged;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService)
        {
            _BuyViewModel = buyViewModel;
            _BuyStockService = buyStockService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                Account account = await _BuyStockService.BuyStock(new Account()
                {
                    Id = 1,
                    Balance = 500,
                    AssetTransactions = new List<AssetTransaction>(),
                }, _BuyViewModel.Symbol, _BuyViewModel.ShareToBuy);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}