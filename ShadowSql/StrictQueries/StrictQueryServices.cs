using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.SqlVales;
using System;
using System.Collections.Generic;

namespace ShadowSql.StrictQueries;

/// <summary>
/// 按列查询服务
/// </summary>
public static partial class StrictQueryServices
{
    #region StrictParameter/StrictValue
    /// <summary>
    /// 对列进行参数化查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id]=@LastId
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictParameter("Id", "=" , "LastId");
    /// </code>
    /// </example>
    public static TQuery StrictParameter<TQuery>(this TQuery query, string columnName, string op = "=", string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CheckParameter(query, columnName, CompareSymbol.Get(op), parameter));
        return query;
    }
    /// <summary>
    /// 对列按值查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <param name="op"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id]>100
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictValue("Id", 100, ">");
    /// </code>
    /// </example>
    public static TQuery StrictValue<TQuery, TValue>(this TQuery query, string columnName, TValue value, string op = "=")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CheckValue(query, columnName, CompareSymbol.Get(op), value));
        return query;
    }
    /// <summary>
    /// 列等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id]=@ParentId
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictEqual("Id", "ParentId");
    /// </code>
    /// </example>
    public static TQuery StrictEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Equal, parameter));
        return query;
    }
    /// <summary>
    /// 列等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id]=100
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictEqualValue("Id", 100);
    /// </code>
    /// </example>
    public static TQuery StrictEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.Equal, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列不等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Status]&lt;>@FailStatus
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotEqual("Status", "FailStatus");
    /// </code>
    /// </example>
    public static TQuery StrictNotEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.NotEqual, parameter));
        return query;
    }
    /// <summary>
    /// 列不等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Status]&lt;>0
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotEqualValue("Status", false);
    /// </code>
    /// </example>
    public static TQuery StrictNotEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.NotEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列大于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]>@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictGreater("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery StrictGreater<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Greater, parameter));
        return query;
    }
    /// <summary>
    /// 列大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]>60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictGreaterValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery StrictGreaterValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.Greater, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列小于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]&lt;@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictLess("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery StrictLess<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Less, parameter));
        return query;
    }
    /// <summary>
    /// 列小于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]&lt;60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictLessValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery StrictLessValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.Less, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列大于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]>=@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictGreaterOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery StrictGreaterEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.GreaterEqual, parameter));
        return query;
    }
    /// <summary>
    /// 列大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]>=60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictGreaterOrValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery StrictGreaterEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.GreaterEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列小于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]&lt;=@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictLessOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery StrictLessEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.LessEqual, parameter));
        return query;
    }
    /// <summary>
    /// 列小于等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score]&lt;=60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictLessOrEqualValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery StrictLessEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.LessEqual, SqlValue.From(value)));
        return query;
    }

    /// <summary>
    /// 列包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] IN @Ids
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictIn("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery StrictIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.In, parameter));
        return query;
    }
    /// <summary>
    /// 列包含于值列表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] IN (1,3,5)
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictInValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery StrictInValue<TQuery, TValue>(this TQuery query, string columnName, IEnumerable<TValue> values)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.In, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 列不包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] NOT IN @Ids
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotIn("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery StrictNotIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.NotIn, parameter));
        return query;
    }
    /// <summary>
    /// 列不包含于值列表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] NOT IN (1,3,5)
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotInValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery StrictNotInValue<TQuery, TValue>(this TQuery query, string columnName, IEnumerable<TValue> values)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.NotIn, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 列是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score] IS NULL
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictIsNull("Score");
    /// </code>
    /// </example>
    public static TQuery StrictIsNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.Strict(columnName)));
        return query;
    }
    /// <summary>
    /// 列不是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Score] IS NOT NULL
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotNull("Score");
    /// </code>
    /// </example>
    public static TQuery StrictNotNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.Strict(columnName)));
        return query;
    }
    /// <summary>
    /// 列匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Title] LIKE @KeyWord
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictLike("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery StrictLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Like, parameter));
        return query;
    }
    /// <summary>
    /// 列匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Name] LIKE '张%'
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictLikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery StrictLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.Like, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列不匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Title] NOT LIKE @KeyWord
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotLike("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery StrictNotLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.NotLike, parameter));
        return query;
    }
    /// <summary>
    /// 列不匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Name] NOT LIKE '张%'
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotLikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery StrictNotLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.Strict(columnName), CompareSymbol.NotLike, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictBetween("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery StrictBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName2(end, begin);
        query.Query.AddLogic(new BetweenLogic(query.Strict(columnName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 列在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] BETWEEN 11 AND 19
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictBetween("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery StrictBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new BetweenLogic(query.Strict(columnName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    /// <summary>
    /// 列不在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] NOT BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotBetween("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery StrictNotBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName(end, begin);
        query.Query.AddLogic(new NotBetweenLogic(query.Strict(columnName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 列不在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <example>
    /// [Id] NOT BETWEEN 11 AND 19
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .StrictNotBetweenValue("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery StrictNotBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new NotBetweenLogic(query.Strict(columnName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    #endregion
    #region TableStrict
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Query TableStrict<Query>(this Query query, string tableName, string columnName, Func<IField, AtomicLogic> compare)
        where Query : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(compare(query.From(tableName).Strict(columnName)));
        return query;
    }
    #endregion
    #region TableStrictParameter/TableStrictValue
    /// <summary>
    /// 对列进行参数化查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictParameter<TQuery>(this TQuery query, string tableName, string columnName, string op = "=", string parameter = "")
        where TQuery : IMultiView, IDataSqlQuery
    {
        query.Query.AddLogic(CheckParameter(query.From(tableName), columnName, CompareSymbol.Get(op), parameter));
        return query;
    }
    /// <summary>
    /// 对列按值查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <param name="op"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value, string op = "=")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CheckValue(query.From(tableName), columnName, CompareSymbol.Get(op), value));
        return query;
    }
    /// <summary>
    /// 列等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.Equal, parameter));
        return query;
    }
    /// <summary>
    /// 列等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.Equal, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列不等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.NotEqual, parameter));
        return query;
    }
    /// <summary>
    /// 列不等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.NotEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列大于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictGreater<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.Greater, parameter));
        return query;
    }
    /// <summary>
    /// 列大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictGreaterValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.Greater, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列小于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictLess<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.Less, parameter));
        return query;
    }
    /// <summary>
    /// 列小于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictLessValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.Less, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列大于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictGreaterEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.GreaterEqual, parameter));
        return query;
    }
    /// <summary>
    /// 列大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictGreaterEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.GreaterEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列小于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictLessEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.LessEqual, parameter));
        return query;
    }
    /// <summary>
    /// 列小于等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictLessEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.LessEqual, SqlValue.From(value)));
        return query;
    }

    /// <summary>
    /// 列包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.In, parameter));
        return query;
    }
    /// <summary>
    /// 列包含于值列表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, IEnumerable<TValue> values)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.In, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 列不包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.NotIn, parameter));
        return query;
    }
    /// <summary>
    /// 列不包含于值列表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, IEnumerable<TValue> values)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.NotIn, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 列是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictIsNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.From(tableName).Strict(columnName)));
        return query;
    }
    /// <summary>
    /// 列不是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.From(tableName).Strict(columnName)));
        return query;
    }
    /// <summary>
    /// 列匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.Like, parameter));
        return query;
    }
    /// <summary>
    /// 列匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.Like, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列不匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), columnName, CompareSymbol.NotLike, parameter));
        return query;
    }
    /// <summary>
    /// 列不匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).Strict(columnName), CompareSymbol.NotLike, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 列在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName2(end, begin);
        query.Query.AddLogic(new BetweenLogic(query.From(tableName).Strict(columnName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 列在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new BetweenLogic(query.From(tableName).Strict(columnName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    /// <summary>
    /// 列不在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName(end, begin);
        query.Query.AddLogic(new NotBetweenLogic(query.From(tableName).Strict(columnName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 列不在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TQuery TableStrictNotBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new NotBetweenLogic(query.From(tableName).Strict(columnName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    #endregion
    /// <summary>
    /// 检查值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="view"></param>
    /// <param name="columnName"></param>
    /// <param name="op"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static AtomicLogic CheckValue<TValue>(ITableView view, string columnName, CompareSymbol op, TValue value)
    {
        var field = view.Strict(columnName);
        if (op == CompareSymbol.IsNull)
            return new IsNullLogic(field);
        else if (op == CompareSymbol.NotNull)
            return new NotNullLogic(field);
        if (value is null)
        {
            if (op == CompareSymbol.Equal)
                return new IsNullLogic(field);
            else if (op == CompareSymbol.NotEqual)
                return new NotNullLogic(field);
            throw new ArgumentNullException(nameof(value));
        }

        return new CompareLogic(field, op, SqlValue.From(value));
    }
    /// <summary>
    /// 检查参数
    /// </summary>
    /// <param name="view"></param>
    /// <param name="columnName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static AtomicLogic CheckParameter(ITableView view, string columnName, CompareSymbol op, string parameter)
    {
        if (op == CompareSymbol.IsNull)
            return new IsNullLogic(view.Strict(columnName));
        else if (op == CompareSymbol.NotNull)
            return new NotNullLogic(view.Strict(columnName));
        return CreateCompareLogic(view, columnName, op, parameter);
    }
    /// <summary>
    /// 创建比较逻辑
    /// </summary>
    /// <param name="view"></param>
    /// <param name="columnName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static CompareLogic CreateCompareLogic(ITableView view, string columnName, CompareSymbol op, string parameter)
    {
        var field = view.Strict(columnName);
        parameter = Parameter.CheckName(parameter, columnName);
        return new CompareLogic(field, op, Parameter.Use(parameter));
    }
}
