using ShadowSql.Arithmetic;
using ShadowSql.Assigns;
using ShadowSql.Compares;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 运算符管理器
/// </summary>
public static class SymbolManager
{
    /// <summary>
    /// 转换为算术运算符
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static ArithmeticSymbol GetArithmeticSymbol(ExpressionType symbol)
    {
        return symbol switch
        {
            ExpressionType.Add => ArithmeticSymbol.Add,
            ExpressionType.Subtract => ArithmeticSymbol.Sub,
            ExpressionType.Multiply => ArithmeticSymbol.Mul,
            ExpressionType.Divide => ArithmeticSymbol.Div,
            ExpressionType.Modulo => ArithmeticSymbol.Mod,
            ExpressionType.And => ArithmeticSymbol.And,
            ExpressionType.Or => ArithmeticSymbol.Or,
            ExpressionType.ExclusiveOr => ArithmeticSymbol.Xor,
            _ => throw new NotSupportedException($"不支持的运算符: {symbol}"),
        };
    }
    /// <summary>
    /// 转换为比较运算符
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static CompareSymbol GetCompareSymbol(ExpressionType symbol)
    {
        return symbol switch
        {
            ExpressionType.Equal => CompareSymbol.Equal,
            ExpressionType.NotEqual => CompareSymbol.NotEqual,
            ExpressionType.GreaterThan => CompareSymbol.Greater,
            ExpressionType.GreaterThanOrEqual => CompareSymbol.GreaterEqual,
            ExpressionType.LessThan => CompareSymbol.Less,
            ExpressionType.LessThanOrEqual => CompareSymbol.LessEqual,
            _ => throw new NotSupportedException($"不支持的运算符: {symbol}"),
        };
    }
    /// <summary>
    /// 转换为赋值运算符
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static AssignSymbol GetAssignSymbol(ExpressionType symbol)
    {
        return symbol switch
        {
            ExpressionType.Assign => AssignSymbol.Assign,
            ExpressionType.AddAssign => AssignSymbol.AddAssign,
            ExpressionType.SubtractAssign => AssignSymbol.SubAssign,
            ExpressionType.MultiplyAssign => AssignSymbol.MulAssign,
            ExpressionType.DivideAssign => AssignSymbol.DivAssign,
            ExpressionType.ModuloAssign => AssignSymbol.ModAssign,
            ExpressionType.AndAssign => AssignSymbol.AndAssign,
            ExpressionType.OrAssign => AssignSymbol.OrAssign,
            ExpressionType.ExclusiveOrAssign => AssignSymbol.XorAssign,
            _ => throw new NotSupportedException($"不支持的运算符: {symbol}"),
        };
    }
}
