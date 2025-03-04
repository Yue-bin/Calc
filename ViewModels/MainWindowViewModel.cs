using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Models;

namespace Calc.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // 两个textblock
    public string SubText
    {
        get
        {
            if (isExpressionMain)
                return _result;
            return _expression;
        }
    }
    public string MainText
    {
        get
        {
            if (isExpressionMain)
                return _expression;
            return _result;
        }
    }

    // 用于切换表达式在主框还是副框
    private bool isExpressionMain = true;

    // 需要计算的和结果
    private string _expression = "18 * 3 + 24 - 1";
    private string _result = "77";

    // 控制按钮
    // C
    private bool CanClear() => _expression.Length > 0 || _result.Length > 0;
    [RelayCommand(CanExecute = nameof(CanClear))]
    private void ClearCommand()
    {
        _expression = string.Empty;
        _result = string.Empty;
    }

    // 退格
    private bool CanDelete() => _expression.Length > 0;
    [RelayCommand(CanExecute = nameof(CanDelete))]
    private void DeleteCommand()
    {
        _expression = _expression[..^1];
    }

    // 求值
    // 把答案换到主文本框
    private bool CanEvaluate() => _expression.Length > 0;
    [RelayCommand(CanExecute = nameof(CanEvaluate))]
    private void EvaluateCommand()
    {
        _result = CalcModel.Evaluate(_expression).ToString();
    }
}
