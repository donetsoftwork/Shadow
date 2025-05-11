using ShadowSql.Aggregates;
using ShadowSql.Assigns;
using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Orders;
using ShadowSql.SingleSelect;
using ShadowSql.SqlVales;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// 字段扩展
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 定位到列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IColumn Column(this ITable table, string columnName)
        => table.GetColumn(columnName)
        ?? throw new ArgumentException(columnName + "列不存在", nameof(columnName));
    /// <summary>
    /// 定位到字段(严格校验)
    /// </summary>
    /// <param name="view"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IField Strict(this ITableView view, string fieldName)
        => view.GetField(fieldName)
        ?? throw new ArgumentException(fieldName + "字段不存在", nameof(fieldName));
    /// <summary>
    /// 定位到字段(宽松不校验)
    /// </summary>
    /// <param name="view"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IField Field(this ITableView view, string fieldName)
        => view.GetField(fieldName) ?? view.NewField(fieldName);
    /// <summary>
    /// 选择列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static IEnumerable<IField> Fields(this ITableView table, params IEnumerable<string> columnNames)
    {
        foreach (var name in columnNames)
            yield return table.Field(name);
    }
    ///// <summary>
    ///// 获取比较字段
    ///// </summary>
    ///// <param name="filter"></param>
    ///// <param name="fieldName"></param>
    ///// <returns></returns>
    //public static ICompareField Compare(this ITableView filter, string fieldName)
    //    => filter.GetCompareField(fieldName);
    ///// <summary>
    ///// 获取比较字段
    ///// </summary>
    ///// <param name="table"></param>
    ///// <param name="fieldName"></param>
    ///// <returns></returns>
    //public static ICompareField GetCompareField(this ITableView table, string fieldName)
    //{
    //    if (table.GetColumn(fieldName) is IColumn column)
    //        return column;
    //    return table.GetField(fieldName);
    //}
    /// <summary>
    /// 获取指定表比较字段
    /// </summary>
    /// <param name="multi"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static ICompareField TableCompare(this IMultiView multi, string tableName, string fieldName)
    {
        var member = multi.From(tableName);
        if (member.GetPrefixField(fieldName) is IPrefixField prefixField)
            return prefixField;
        return member.NewField(fieldName);
    }
    #region Prefix
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="column"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static PrefixField Prefix(this IColumn column, string tableName)
        => new(column, tableName, ".");
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="column"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static IPrefixField Prefix(this IColumn column, IAliasTable table)
        => table.GetPrefixField(column) ?? table.NewPrefixField(column);
    /// <summary>
    /// 定位到前缀字段
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IAliasTable table, string columnName)
        => table.GetPrefixField(columnName)
        ?? throw new ArgumentException(columnName + "字段不存在", nameof(columnName));
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IAliasTable table, IColumn column)
        => table.GetPrefixField(column)
        ?? throw new ArgumentException("字段不存在", nameof(column));
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="join"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IJoinOn join, IColumn column)
    {
        if(column is IPrefixField prefixField)
            return prefixField;
        var left = join.Left;
        var right = join.JoinSource;
        foreach (var c in right.Target.Columns)
            if (c == column)
                return right.Prefix(c);
        foreach (var c in left.Target.Columns)
            if (c == column)
                return left.Prefix(c);
        return right.GetPrefixField(column)
            ?? left.GetPrefixField(column)
            ?? throw new ArgumentException("字段不存在", nameof(column));
    }
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="join"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IJoinOn join, string columnName)
        => join.GetRightField(columnName)
        ?? join.GetLeftField(columnName)
        ?? throw new ArgumentException(columnName + "字段不存在", nameof(columnName));
    /// <summary>
    /// 选择列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static IPrefixField Prefix<TTable>(this IAliasTable<TTable> table, Func<TTable, IColumn> query)
        where TTable : ITable
        => table.GetPrefixField(query(table.Target))
        ?? throw new ArgumentException("字段不存在", nameof(query));
    #endregion
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
    public static AtomicLogic Compare(this ICompareView field, string op, ISqlValue right)
        => new CompareLogic(field, CompareSymbol.Get(op), right);
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="compare"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareView field, CompareSymbol compare, ISqlValue right)
        => new CompareLogic(field, compare, right);
    #endregion
    #region 比较字段逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Equal(this ICompareView field, ISqlValue right)
        => new CompareLogic(field, CompareSymbol.Equal, right);
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqual(this ICompareView field, ISqlValue right)
        => new CompareLogic(field, CompareSymbol.NotEqual, right);
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Greater(this ICompareView field, ISqlValue right)
        => new CompareLogic(field, CompareSymbol.Greater, right);
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Less(this ICompareView field, ISqlValue right)
        => new CompareLogic(field, CompareSymbol.Less, right);
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqual(this ICompareView field, ISqlValue right)
        => new CompareLogic(field, CompareSymbol.GreaterEqual, right);
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqual(this ICompareView field, ISqlValue right)
        => new CompareLogic(field, CompareSymbol.LessEqual, right);
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic BetweenValue(this ICompareView field, ISqlValue begin, ISqlValue end)
        => new BetweenLogic(field, begin, end);
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetweenValue(this ICompareView field, ISqlValue begin, ISqlValue end)
        => new NotBetweenLogic(field, begin, end);
    #endregion
    #endregion
    #region 子查询
    /// <summary>
    /// 子查询作为字段
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SingleField ToField(this ISingleSelect select)
        => new(select);
    /// <summary>
    /// 子查询作为字段别名
    /// </summary>
    /// <param name="select"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static SingleFieldAlias ToField(this ISingleSelect select, string alias)    
        => new(select, alias);
    #endregion
    #region 赋值
    #region 参数
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation EqualTo(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.EqualTo, Parameter.Use(parameter, field));
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation Add(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Add, Parameter.Use(parameter, field));
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation Sub(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Sub, Parameter.Use(parameter, field));
    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation Mul(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Mul, Parameter.Use(parameter, field));
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation Div(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Div, Parameter.Use(parameter, field));
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation Mod(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Mod, Parameter.Use(parameter, field));
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation And(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.And, Parameter.Use(parameter, field));
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation Or(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Or, Parameter.Use(parameter, field));
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="field"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AssignOperation Xor(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Xor, Parameter.Use(parameter, field));
    #endregion
    #region 数据库值
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation EqualTo(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.EqualTo, right);
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Add(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.Add, right);
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Sub(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.Sub, right);
    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Mul(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.Mul, right);
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Div(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.Div, right);
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Mod(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.Mod, right);
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation And(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.And, right);
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Or(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.Or, right);
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Xor(this IAssignView field, ISqlValue right)
        => new(field, AssignSymbol.Xor, right);
    #endregion
    #region 值
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation EqualToValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.EqualTo, SqlValue.From(value));
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation AddValue<TValue>(this IAssignView field, TValue value)
    => new(field, AssignSymbol.Add, SqlValue.From(value));
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation SubValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.Sub, SqlValue.From(value));
    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation MulValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.Mul, SqlValue.From(value));
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation DivValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.Div, SqlValue.From(value));
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation ModValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.Mod, SqlValue.From(value));
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation AndValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.And, SqlValue.From(value));
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation OrValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.Or, SqlValue.From(value));
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation XorValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.Xor, SqlValue.From(value));
    #endregion
    #endregion
    #region 插入
    #region 插入值
    /// <summary>
    /// 插入单值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static InsertValue InsertValue<TValue>(this IColumn column, TValue value)
        => new(column, SqlValue.From(value));

    /// <summary>
    /// 插入多值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static InsertValues InsertValues<TValue>(this IColumn column, params IEnumerable<TValue> values)
        => new(column, [.. values.Select(value => SqlValue.From(value))]);
    #endregion
    #region 插入参数
    /// <summary>
    /// 插入参数
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    public static InsertValue Insert(this IColumn column, string parameterName = "")
         => new(column, Parameter.Use(parameterName, column));
    #endregion
    #endregion
    #region 排序
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IOrderDesc Desc(this IOrderAsc field)
        => new DescView(field);
    #endregion
}
