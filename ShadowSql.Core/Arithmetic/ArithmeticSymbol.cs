using ShadowSql.Engines;
using ShadowSql.Fragments;
using System;
using System.Text;

namespace ShadowSql.Arithmetic;

/// <summary>
/// 算术运算符
/// </summary>
public sealed class ArithmeticSymbol : ISqlEntity
{
    /// <summary>
    /// 算术运算符
    /// </summary>
    private ArithmeticSymbol(char operation)
    {
        _operation = operation;
    }
    #region 配置
    private readonly char _operation ;
    /// <summary>
    /// 符号
    /// </summary>
    public char Operation
        => _operation;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    internal void WriteCore(ISqlEngine engine, StringBuilder sql)
        => sql.Append(_operation);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteCore(engine, sql);
    #region Manager
    /// <summary>
    /// 加
    /// </summary>
    public static ArithmeticSymbol Add => Manager.Add;
    /// <summary>
    /// 减
    /// </summary>
    public static ArithmeticSymbol Sub => Manager.Sub;
    /// <summary>
    /// 乘
    /// </summary>
    public static ArithmeticSymbol Mul => Manager.Mul;
    /// <summary>
    /// 除
    /// </summary>
    public static ArithmeticSymbol Div => Manager.Div;
    /// <summary>
    /// 取模
    /// </summary>
    public static ArithmeticSymbol Mod => Manager.Mod;
    /// <summary>
    /// 位与
    /// </summary>
    public static ArithmeticSymbol And => Manager.And;
    /// <summary>
    /// 位或
    /// </summary>
    public static ArithmeticSymbol Or => Manager.Or;
    /// <summary>
    /// 异或
    /// </summary>
    public static ArithmeticSymbol Xor => Manager.Xor;
    /// <summary>
    /// 获取操作符
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static ArithmeticSymbol Get(string operation)
    {
        return operation switch
        {
            "+" => Manager.Add,
            "-" => Manager.Sub,
            "*" => Manager.Mul,
            "/" => Manager.Div,
            "%" => Manager.Mod,
            "&" => Manager.And,
            "|" => Manager.Or,
            "^" => Manager.Xor,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "不支持的运算符"),
        };
    }
    #endregion
    /// <summary>
    /// 操作符管理
    /// </summary>
    class Manager
    {
        /// <summary>
        /// 加
        /// </summary>
        internal static readonly ArithmeticSymbol Add = new('+');
        /// <summary>
        /// 减
        /// </summary>
        internal static readonly ArithmeticSymbol Sub = new('-');
        /// <summary>
        /// 乘
        /// </summary>
        internal static readonly ArithmeticSymbol Mul = new('*');
        /// <summary>
        /// 除
        /// </summary>
        internal static readonly ArithmeticSymbol Div = new('/');
        /// <summary>
        /// 取模
        /// </summary>
        internal static readonly ArithmeticSymbol Mod = new('%');
        /// <summary>
        /// 位与
        /// </summary>
        internal static readonly ArithmeticSymbol And = new('&');
        /// <summary>
        /// 位或
        /// </summary>
        internal static readonly ArithmeticSymbol Or = new('|');
        /// <summary>
        /// 异或
        /// </summary>
        internal static readonly ArithmeticSymbol Xor = new('^');
    }
}
