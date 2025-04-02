using ShadowSql.Aggregates;
using ShadowSql.Assigns;
using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.FieldInfos;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Logics;
using ShadowSql.Orders;
using ShadowSql.SingleSelect;
using ShadowSql.SqlVales;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// Column扩展
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 定位到列
    /// </summary>
    /// <param name="view"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IColumn Column(this ITableView view, string columnName)
        => view.GetColumn(columnName)
        ?? throw new ArgumentException("列不存在", nameof(columnName));
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static ICompareField Compare(this IDataFilter filter, string fieldName)
        => filter.GetCompareField(fieldName);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static ICompareField GetCompareField(this ITableView table,  string fieldName)
    {
        if(table.GetColumn(fieldName) is IColumn column)
            return column;
        return table.Field(fieldName);
    }
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
        if (member.GetPrefixColumn(fieldName) is IPrefixColumn prefixColumn)
            return prefixColumn;
        return member.Field(fieldName);
    }
    /// <summary>
    /// 定位到前缀列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixColumn PrefixColumn(this IAliasTable table, string columnName)
        => table.GetPrefixColumn(columnName)
        ?? throw new ArgumentException("列不存在", nameof(columnName));
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixColumn Prefix(this IAliasTable table, IColumn column)
        => table.GetPrefixColumn(column)
        ?? throw new ArgumentException("列不存在", nameof(column));
    #region Count
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static CountFieldInfo Count(this ITableView view)
        => CountFieldInfo.Instance;
    #endregion
    #region CountAs
    /// <summary>
    /// 计数别名
    /// </summary>
    /// <param name="view"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static CountAliasFieldInfo CountAs(this ITableView view, string alias = "Count")
        => CountAliasFieldInfo.Use(alias);
    #endregion
    #region DistinctCount
    /// <summary>
    /// 列去重统计
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public static DistinctCountFieldInfo DistinctCount(this IColumn column)
        => new(column);
    /// <summary>
    /// 字段去重统计
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    public static DistinctCountFieldInfo DistinctCount(this IField field)
        => new(field);
    #endregion
    #region DistinctCountAs
    /// <summary>
    /// 列去重统计
    /// </summary>
    /// <param name="column"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static DistinctCountAliasFieldInfo DistinctCountAs(this IColumn column, string alias = "Count")
        => new(column, alias);
    /// <summary>
    /// 字段去重统计
    /// </summary>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static DistinctCountAliasFieldInfo DistinctCountAs(this IField field, string alias = "Count")
        => new(field, alias);
    #endregion
    #region ICompareField
    #region 运算符比较参数逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareField column, string op, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Get(op), Parameter.Use(parameter, column));
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="compare"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareField column, CompareSymbol compare, string parameter = "")
        => new CompareLogic(column, compare, Parameter.Use(parameter, column));
    #endregion
    #region 比较参数逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Equal(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Equal, Parameter.Use(parameter, column));
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqual(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.NotEqual, Parameter.Use(parameter, column));
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Greater(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Greater, Parameter.Use(parameter, column));
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Less(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Less, Parameter.Use(parameter, column));
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqual(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.GreaterEqual, Parameter.Use(parameter, column));
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqual(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.LessEqual, Parameter.Use(parameter, column));
    /// <summary>
    /// Like匹配逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Like(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Like, Parameter.Use(parameter, column));
    /// <summary>
    /// 否定Like匹配逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotLike(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.NotLike, Parameter.Use(parameter, column));
    /// <summary>
    /// 包含逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic In(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.In, Parameter.Use(parameter, column));
    /// <summary>
    /// 不包含逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotIn(this ICompareField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.NotIn, Parameter.Use(parameter, column));
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic Between(this ICompareField column, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, column);
        var parameter2 = Parameter.Use(end, parameter);
        return new BetweenLogic(column, parameter, parameter2);
    }
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetween(this ICompareField column, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, column);
        var parameter2 = Parameter.Use(end, parameter);
        return new NotBetweenLogic(column, parameter, parameter2);
    }
    #endregion
    #endregion
    #region IAggregateField
    #region 运算符比较参数逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this IAggregateField column, string op, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Get(op), Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="compare"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this IAggregateField column, CompareSymbol compare, string parameter = "")
        => new CompareLogic(column, compare, Parameter.Use(parameter, column.TargetName));
    #endregion
    #region 比较参数逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Equal(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Equal, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqual(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.NotEqual, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Greater(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Greater, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Less(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Less, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqual(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.GreaterEqual, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqual(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.LessEqual, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// Like匹配逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic Like(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.Like, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 否定Like匹配逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotLike(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.NotLike, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 包含逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic In(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.In, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 不包含逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static AtomicLogic NotIn(this IAggregateField column, string parameter = "")
        => new CompareLogic(column, CompareSymbol.NotIn, Parameter.Use(parameter, column.TargetName));
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic Between(this IAggregateField column, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, column.TargetName);
        var parameter2 = Parameter.Use(end, parameter);
        return new BetweenLogic(column, parameter, parameter2);
    }
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetween(this IAggregateField column, string begin = "", string end = "")
    {
        var parameter = Parameter.Use(begin, column.TargetName);
        var parameter2 = Parameter.Use(end, parameter);
        return new NotBetweenLogic(column, parameter, parameter2);
    }
    #endregion
    #endregion
    #region ICompareView
    #region 运算符比较值逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="op"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic CompareValue<TValue>(this ICompareView column, string op, TValue value)
        => new CompareLogic(column, CompareSymbol.Get(op), SqlValue.From(value));
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="compare"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic CompareValue<TValue>(this ICompareView column, CompareSymbol compare, TValue value)
        => new CompareLogic(column, compare, SqlValue.From(value));
    #endregion
    #region 无参数逻辑
    /// <summary>
    /// 是否为空逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public static AtomicLogic IsNull(this ICompareView column)
        => new IsNullLogic(column);
    /// <summary>
    /// 是否不为空逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public static AtomicLogic NotNull(this ICompareView column)
        => new NotNullLogic(column);
    #endregion
    #region 比较值逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic EqualValue<TValue>(this ICompareView column, TValue value)
        => new CompareLogic(column, CompareSymbol.Equal, SqlValue.From(value));
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqualValue<TValue>(this ICompareView column, TValue value)
        => new CompareLogic(column, CompareSymbol.NotEqual, SqlValue.From(value));
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterValue<TValue>(this ICompareView column, TValue value)
        => new CompareLogic(column, CompareSymbol.Greater, SqlValue.From(value));
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic LessValue<TValue>(this ICompareView column, TValue value)
        => new CompareLogic(column, CompareSymbol.Less, SqlValue.From(value));
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqualValue<TValue>(this ICompareView column, TValue value)
        => new CompareLogic(column, CompareSymbol.GreaterEqual, SqlValue.From(value));
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqualValue<TValue>(this ICompareView column, TValue value)
        => new CompareLogic(column, CompareSymbol.LessEqual, SqlValue.From(value));
    /// <summary>
    /// Like匹配逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic LikeValue(this ICompareView column, string value)
        => new CompareLogic(column, CompareSymbol.Like, SqlValue.From(value));
    /// <summary>
    /// 否定Like匹配逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AtomicLogic NotLikeValue(this ICompareView column, string value)
        => new CompareLogic(column, CompareSymbol.NotLike, SqlValue.From(value));
    /// <summary>
    /// 包含逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static AtomicLogic InValue<TValue>(this ICompareView column, params TValue[] values)
        => new CompareLogic(column, CompareSymbol.In, SqlValue.Values(values));
    /// <summary>
    /// 不包含逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static AtomicLogic NotInValue<TValue>(this ICompareView column, params TValue[] values)
        => new CompareLogic(column, CompareSymbol.NotIn, SqlValue.Values(values));
    /// <summary>
    /// 在...之间逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic BetweenValue<TValue>(this ICompareView column, TValue begin, TValue end)
        => new BetweenLogic(column, SqlValue.From(begin), SqlValue.From(end));
    /// <summary>
    /// 不在...之间逻辑
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static AtomicLogic NotBetweenValue<TValue>(this ICompareView column, TValue begin, TValue end)
        => new NotBetweenLogic(column, SqlValue.From(begin), SqlValue.From(end));
    #endregion
    #region 运算符比较字段逻辑
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="op"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareView column, string op, ISqlValue right)
        => new CompareLogic(column, CompareSymbol.Get(op), right);
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="compare"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Compare(this ICompareView column, CompareSymbol compare, ISqlValue right)
        => new CompareLogic(column, compare, right);
    #endregion
    #region 比较字段逻辑
    /// <summary>
    /// 相等逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Equal(this ICompareView column, ISqlValue right)
        => new CompareLogic(column, CompareSymbol.Equal, right);
    /// <summary>
    /// 不相等逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic NotEqual(this ICompareView column, ISqlValue right)
        => new CompareLogic(column, CompareSymbol.NotEqual, right);
    /// <summary>
    /// 大于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Greater(this ICompareView column, ISqlValue right)
        => new CompareLogic(column, CompareSymbol.Greater, right);
    /// <summary>
    /// 小于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic Less(this ICompareView column, ISqlValue right)
        => new CompareLogic(column, CompareSymbol.Less, right);
    /// <summary>
    /// 大于等于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic GreaterEqual(this ICompareView column, ISqlValue right)
        => new CompareLogic(column, CompareSymbol.GreaterEqual, right);
    /// <summary>
    /// 小于等于逻辑
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AtomicLogic LessEqual(this ICompareView column, ISqlValue right)
        => new CompareLogic(column, CompareSymbol.LessEqual, right);
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
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation EqualTo(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.EqualTo, right);
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Add(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.Add, right);
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Sub(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.Sub, right);
    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Mul(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.Mul, right);
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Div(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.Div, right);
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Mod(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.Mod, right);
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation And(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.And, right);
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Or(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.Or, right);
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="column"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AssignOperation Xor(this IColumn column, ISqlValue right)
        => new(column, AssignSymbol.Xor, right);
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation EqualToValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.EqualTo, SqlValue.From(value));
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation AddValue<TValue>(this IColumn column, TValue value)
    => new(column, AssignSymbol.Add, SqlValue.From(value));
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation SubValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.Sub, SqlValue.From(value));
    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation MulValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.Mul, SqlValue.From(value));
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation DivValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.Div, SqlValue.From(value));
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation ModValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.Mod, SqlValue.From(value));
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation AndValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.And, SqlValue.From(value));
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation OrValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.Or, SqlValue.From(value));
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AssignOperation XorValue<TValue>(this IColumn column, TValue value)
        => new(column, AssignSymbol.Xor, SqlValue.From(value));
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
        => new(column, [.. values.Select(value=> SqlValue.From(value))]);
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
    #region 聚合
    #region IAggregateField
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Count(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Count);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Sum(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Sum);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Avg(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Avg);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Max(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Max);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Min(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Min);
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias CountAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Count, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias SumAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Sum, alias);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias AvgAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Avg, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias MaxAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Max, alias);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias MinAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Min, alias);
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
