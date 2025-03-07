using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calc.Models;
using System.ComponentModel;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace Calc.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // 两个textblock
    public string SubText
    {
        get
        {
            if (IsExpressionMain)
                return _result;
            return _expression;
        }
    }
    public string MainText
    {
        get
        {
            if (IsExpressionMain)
                return _expression;
            return _result;
        }
    }

    // 用于切换表达式在主框还是副框
    private bool isExpressionMain = true;
    private bool IsExpressionMain
    {
        get => isExpressionMain;
        set
        {
            isExpressionMain = value;
            OnPropertyChanged(nameof(MainText));
            OnPropertyChanged(nameof(SubText));
        }
    }

    // 需要计算的和结果
    private string Expression
    {
        get => _expression;
        set
        {
            _expression = value;
            _result = CalcModel.CheckExpression(_expression) ? CalcModel.Evaluate(_expression).ToString() : "0";
            OnPropertyChanged(nameof(MainText));
            OnPropertyChanged(nameof(SubText));
        }
    }
    private string Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged(nameof(MainText));
            OnPropertyChanged(nameof(SubText));
        }
    }
    private string _expression = "0";
    private string _result = "0";

    // 控制按钮
    // C
    public bool CanClear() => _expression.Length > 0 || _result.Length > 0;
    [RelayCommand(CanExecute = nameof(CanClear))]
    public void ClearCommand()
    {
        Expression = string.Empty;
        Result = string.Empty;
    }

    // 退格
    public bool CanDelete() => _expression.Length > 0;
    [RelayCommand(CanExecute = nameof(CanDelete))]
    public void DeleteCommand()
    {
        if (_expression.Length > 1)
            Expression = Expression[..^1];
        else
            Expression = "0";
    }

    // 求值
    // 把答案换到主文本框
    public bool CanEvaluate() => _expression.Length > 0;
    [RelayCommand(CanExecute = nameof(CanEvaluate))]
    public void EvaluateCommand()
    {
        IsExpressionMain = false;
    }

    //内容按钮
    [RelayCommand]
    public void AddCommand(string value)
    {
        if (Expression == "0" && value != ".")
        {
            Expression = string.Empty;
        }
        if (IsExpressionMain)
        {
            Expression += value;
        }
        else
        {
            Expression += value;
            IsExpressionMain = true;
        }
    }
}
