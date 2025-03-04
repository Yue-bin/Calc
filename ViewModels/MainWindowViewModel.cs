using CommunityToolkit.Mvvm.ComponentModel;

namespace Calc.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    public string _subText = "Sub";
    [ObservableProperty]
    public string _mainText = "Main";
}
