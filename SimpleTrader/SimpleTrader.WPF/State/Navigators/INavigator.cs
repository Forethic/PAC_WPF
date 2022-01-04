using SimpleTrader.WPF.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Portfolio,
        Buy,
        Login
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}