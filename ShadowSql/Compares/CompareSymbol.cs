using ShadowSql.Engines;
using ShadowSql.Fragments;
using System;
using System.Text;

namespace ShadowSql.Compares;

/// <summary>
/// 比较逻辑运算符
/// </summary>
/// <example>
/// <code>
/// var compares = ['='，'>'];
/// </code>
/// </example>
public sealed class CompareSymbol : ISqlEntity
{
    private readonly string _operation;
    /// <summary>
    /// 否定运算
    /// </summary>
    internal CompareSymbol _not;
    /// <summary>
    /// 前后交换运算
    /// </summary>
    internal CompareSymbol _reverse;
    /// <summary>
    /// 操作符
    /// </summary>
    public string Operation 
        => _operation;
#pragma warning disable 8618
    private CompareSymbol(string operation)
    {
        _operation = operation;
    }
#pragma warning restore 8618
    #region Manager
    /// <summary>
    /// 操作符管理
    /// </summary>
    private readonly static Lazy<CompareManager> _manager = new(CompareManager.Create);
    /// <summary>
    /// 操作符管理
    /// </summary>
    public static CompareManager Manager
    => _manager.Value;
    /// <summary>
    /// 获取操作符
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static CompareSymbol Get(string operation)
        => _manager.Value.Get(operation);
    /// <summary>
    /// 相等
    /// </summary>
    public static CompareSymbol Equal
        => _manager.Value.Equal;
    /// <summary>
    /// 不等
    /// </summary>
    public static CompareSymbol NotEqual
        => _manager.Value.NotEqual;
    /// <summary>
    /// 小于等于
    /// </summary>
    public static CompareSymbol Greater
        => _manager.Value.Greater;
    /// <summary>
    /// 小于等于
    /// </summary>
    public static CompareSymbol LessEqual
        => _manager.Value.LessEqual;
    /// <summary>
    /// 大于等于
    /// </summary>
    public static CompareSymbol Less
        => _manager.Value.Less;
    /// <summary>
    /// 大于等于
    /// </summary>
    public static CompareSymbol GreaterEqual
        => _manager.Value.GreaterEqual;
    /// <summary>
    /// 匹配
    /// </summary>
    public static CompareSymbol Like
        => _manager.Value.Like;
    /// <summary>
    /// 不匹配
    /// </summary>
    public static CompareSymbol NotLike
        => _manager.Value.NotLike;
    /// <summary>
    /// 在...之间
    /// </summary>
    public static CompareSymbol Between
        => _manager.Value.Between;
    /// <summary>
    /// 不在...之间
    /// </summary>
    public static CompareSymbol NotBetween
        => _manager.Value.NotBetween;
    /// <summary>
    /// 包含
    /// </summary>
    public static CompareSymbol In
        => _manager.Value.In;
    /// <summary>
    /// 不包含
    /// </summary>
    public static CompareSymbol NotIn
        => _manager.Value.NotIn;
    /// <summary>
    /// IS NULL
    /// </summary>
    public static CompareSymbol IsNull
        => _manager.Value.IsNull;
    /// <summary>
    /// IS NOT NULL
    /// </summary>
    public static CompareSymbol NotNull
        => _manager.Value.NotNull;
    /// <summary>
    /// EXISTS
    /// </summary>
    public static CompareSymbol Exists
       => _manager.Value.Exists;
    /// <summary>
    /// NOT EXISTS
    /// </summary>
    public static CompareSymbol NotExists
        => _manager.Value.NotExists;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
        => sql.Append(_operation);
    /// <summary>
    /// 构造一对操作符
    /// </summary>
    /// <param name="leftOperation"></param>
    /// <param name="rightOperation"></param>
    /// <returns></returns>
    internal static (CompareSymbol, CompareSymbol) CreatePair(string leftOperation, string rightOperation)
    {
        var left = new CompareSymbol(leftOperation);
        var right = new CompareSymbol(rightOperation);
        //大部分运算符的否定和交换是一致的
        //Equal、NotEqual的交换是自身
        //IsNull、NotNull为单元运算,交换只能是自身
        left._not = left._reverse = right;
        right._not = right._reverse = left;
        return (left, right);
    }
    /// <summary>
    /// 否定运算
    /// </summary>
    public CompareSymbol Not()
        => _not;
    /// <summary>
    /// 前后交换运算
    /// </summary>
    /// <returns></returns>
    public CompareSymbol Reverse()
        => _reverse;
}
