﻿using System.ComponentModel;
using System.Windows.Input;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.Models;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {

        private ViewModelBase _CurrentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                _CurrentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);


    }
}