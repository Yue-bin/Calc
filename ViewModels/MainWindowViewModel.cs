using CommunityToolkit.Mvvm.ComponentModel;

namespace Calc.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    public string _subText = "77";
    [ObservableProperty]
    public string _mainText = "18 * 3 + 24 - 1";
}
