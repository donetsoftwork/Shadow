using ShadowSql.Aggregates;
using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SqlVales;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 比较逻辑扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region ICompareField
    #region 运算符比较参数逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareField field, string op, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Get(op), Parameter.Use(parameter, field));
    #endregion
    #region 比较参数逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Equal(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Equal, Parameter.Use(parameter, field));
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqual(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.NotEqual, Parameter.Use(parameter, field));
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Greater(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Greater, Parameter.Use(parameter, field));
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Less(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Less, Parameter.Use(parameter, field));
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqual(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.GreaterEqual, Parameter.Use(parameter, field));
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqual(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.LessEqual, Parameter.Use(parameter, field));
    /// <summary>
    /// Like匹配逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Like(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Like, Parameter.Use(parameter, field));
    /// <summary>
    /// 否定Like匹配逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotLike(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.NotLike, Parameter.Use(parameter, field));
    /// <summary>
    /// 包含逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic In(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.In, Parameter.Use(parameter, field));
    /// <summary>
    /// 不包含逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotIn(this ICompareField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.NotIn, Parameter.Use(parameter, field));
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic Between(this ICompareField field, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, field);
        var parameter2 = Parameter.Use(end, parameter);
        return new BetweenLogic(field, parameter, parameter2);
    }
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetween(this ICompareField field, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, field);
        var parameter2 = Parameter.Use(end, parameter);
        return new NotBetweenLogic(field, parameter, parameter2);
    }
    #endregion
    #endregion
    #region IAggregateField
    #region 运算符比较参数逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this IAggregateField field, string op, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Get(op), Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="compare"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this IAggregateField field, CompareSymbol compare, string parameter = "")
        => new CompareLogic(field, compare, Parameter.Use(parameter, field.TargetName));
    #endregion
    #region 比较参数逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Equal(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Equal, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqual(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.NotEqual, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Greater(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Greater, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Less(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Less, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqual(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.GreaterEqual, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqual(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.LessEqual, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// Like匹配逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Like(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.Like, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 否定Like匹配逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotLike(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.NotLike, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 包含逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic In(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.In, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 不包含逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotIn(this IAggregateField field, string parameter = "")
        => new CompareLogic(field, CompareSymbol.NotIn, Parameter.Use(parameter, field.TargetName));
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic Between(this IAggregateField field, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, field.TargetName);
        var parameter2 = Parameter.Use(end, parameter);
        return new BetweenLogic(field, parameter, parameter2);
    }
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetween(this IAggregateField field, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, field.TargetName);
        var parameter2 = Parameter.Use(end, parameter);
        return new NotBetweenLogic(field, parameter, parameter2);
    }
    #endregion
    #endregion
    #region ICompareView
    #region 运算符比较值逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="op"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic CompareValue<TValue>(this ICompareView field, string op, TValue value)
        => new CompareLogic(field, CompareSymbol.Get(op), SqlValue.From(value));
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="compare"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic CompareValue<TValue>(this ICompareView field, CompareSymbol compare, TValue value)
        => new CompareLogic(field, compare, SqlValue.From(value));
    #endregion
    #region 无参逻辑
    /// <summary>
    /// 是否为空逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    public static AtomicLogic IsNull(this ICompareView field)
        => new IsNullLogic(field);
    /// <summary>
    /// 是否不为空逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    public static AtomicLogic NotNull(this ICompareView field)
        => new NotNullLogic(field);
    #endregion
    #region 比较值逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic EqualValue<TValue>(this ICompareView field, TValue value)
        => new CompareLogic(field, CompareSymbol.Equal, SqlValue.From(value));
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqualValue<TValue>(this ICompareView field, TValue value)
        => new CompareLogic(field, CompareSymbol.NotEqual, SqlValue.From(value));
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterValue<TValue>(this ICompareView field, TValue value)
        => new CompareLogic(field, CompareSymbol.Greater, SqlValue.From(value));
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic LessValue<TValue>(this ICompareView field, TValue value)
        => new CompareLogic(field, CompareSymbol.Less, SqlValue.From(value));
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqualValue<TValue>(this ICompareView field, TValue value)
        => new CompareLogic(field, CompareSymbol.GreaterEqual, SqlValue.From(value));
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqualValue<TValue>(this ICompareView field, TValue value)
        => new CompareLogic(field, CompareSymbol.LessEqual, SqlValue.From(value));
    /// <summary>
    /// Like匹配逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic LikeValue(this ICompareView field, string value)
        => new CompareLogic(field, CompareSymbol.Like, SqlValue.From(value));
    /// <summary>
    /// 否定Like匹配逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic NotLikeValue(this ICompareView field, string value)
        => new CompareLogic(field, CompareSymbol.NotLike, SqlValue.From(value));
    /// <summary>
    /// 包含逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static AtomicLogic InValue<TValue>(this ICompareView field, params IEnumerable<TValue> values)
        => new CompareLogic(field, CompareSymbol.In, SqlValue.Values(values));
    /// <summary>
    /// 不包含逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static AtomicLogic NotInValue<TValue>(this ICompareView field, params IEnumerable<TValue> values)
        => new CompareLogic(field, CompareSymbol.NotIn, SqlValue.Values(values));
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic BetweenValue<TValue>(this ICompareView field, TValue begin, TValue end)
        => new BetweenLogic(field, SqlValue.From(begin), SqlValue.From(end));
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetweenValue<TValue>(this ICompareView field, TValue begin, TValue end)
        => new NotBetweenLogic(field, SqlValue.From(begin), SqlValue.From(end));
    #endregion
    #region 运算符比较字段逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="op"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareView field, string op, ICompareView right)
        => new CompareLogic(field, CompareSymbol.Get(op), right);
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="compare"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareView field, CompareSymbol compare, ICompareView right)
        => new CompareLogic(field, compare, right);
    #endregion
    #region 比较字段逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Equal(this ICompareView field, ICompareView right)
        => new CompareLogic(field, CompareSymbol.Equal, right);
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqual(this ICompareView field, ICompareView right)
        => new CompareLogic(field, CompareSymbol.NotEqual, right);
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Greater(this ICompareView field, ICompareView right)
        => new CompareLogic(field, CompareSymbol.Greater, right);
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Less(this ICompareView field, ICompareView right)
        => new CompareLogic(field, CompareSymbol.Less, right);
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqual(this ICompareView field, ICompareView right)
        => new CompareLogic(field, CompareSymbol.GreaterEqual, right);
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqual(this ICompareView field, ICompareView right)
        => new CompareLogic(field, CompareSymbol.LessEqual, right);
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic BetweenValue(this ICompareView field, ICompareView begin, ICompareView end)
        => new BetweenLogic(field, begin, end);
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetweenValue(this ICompareView field, ICompareView begin, ICompareView end)
        => new NotBetweenLogic(field, begin, end);
    #endregion
    #endregion
}
