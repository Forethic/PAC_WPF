using System;
using System.Windows.Input;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _Navigator;
        private readonly ISimpleTraderViewModelAbstractFactory _ViewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, ISimpleTraderViewModelAbstractFactory viewModelFactory)
        {
            _Navigator = navigator;
            _ViewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType type)
            {
                _Navigator.CurrentViewModel = _ViewModelFactory.CreateViewModel(type);
            }
        }
    }
}