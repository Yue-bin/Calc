using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Calc.Models;

public class CalcModel
{
    public static double Evaluate(string expression)
    {
        string processed = expression.Replace("×", "*").Replace("÷", "/");
        processed = Regex.Replace(processed, @"(\d+\.?\d*)%", "$1*0.01");
        processed = System.Text.RegularExpressions.Regex.Replace(
            processed,
            @"(?<!\d\.)\b(\d+)\b(?!\.\d)",
            "$1.0"
        );

        return Convert.ToDouble(new DataTable().Compute(processed, null));
    }

    public static bool CheckExpression(string expression)
    {
        if (string.IsNullOrEmpty(expression)) return false;

        // 1. 非法字符检查
        if (!Regex.IsMatch(expression, @"^[\d%\×\÷+\-\(\)\.]+$"))
            return false;

        // 2. 括号匹配检查
        Stack<char> brackets = new Stack<char>();
        foreach (char c in expression)
        {
            if (c == '(') brackets.Push(c);
            else if (c == ')')
            {
                if (brackets.Count == 0) return false;
                brackets.Pop();
            }
        }
        if (brackets.Count != 0) return false;

        // 3. 百分号检查
        if (Regex.IsMatch(expression, @"(?<!\d)%|%(?=\d|\.)"))
            return false;

        // 4. 小数点检查
        if (Regex.IsMatch(expression, @"(^\.|\.$)|(\d*\.\d*\.)"))
            return false;

        // 5. 运算符位置检查
        if ("×÷+%)".Contains(expression[0]) ||
            "×÷+-%(".Contains(expression[^1]))
            return false;

        // 6. 连续运算符检查
        if (Regex.IsMatch(expression, @"[×÷+%-][×÷+%]"))
            return false;

        return true;
    }
}