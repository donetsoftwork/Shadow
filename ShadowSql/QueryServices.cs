using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.SqlVales;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 链式查询
/// </summary>
public static partial class ShadowSqlServices
{
    #region ApplyQuery
    /// <summary>
    /// 切换为And
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <returns></returns>
    public static Query ToAnd<Query>(this Query dataQuery)
        where Query : IDataQuery
    {
        dataQuery.ApplyQuery(query => query.ToAnd());
        return dataQuery;
    }
    /// <summary>
    /// 切换为Or
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <returns></returns>
    public static Query ToOr<Query>(this Query dataQuery)
        where Query : IDataQuery
    {
        dataQuery.ApplyQuery(query => query.ToOr());
        return dataQuery;
    }
    #endregion
    #region 基础查询功能
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query dataQuery, params IEnumerable<string> conditions)
        where Query : IDataQuery
    {
        dataQuery.AddConditions(conditions);
        return dataQuery;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query dataQuery, AtomicLogic logic)
        where Query : IDataQuery
    {
        dataQuery.AddLogic(logic);
        return dataQuery;
    }
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="columnName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static Query Column<Query>(this Query dataQuery, string columnName, Func<ICompareField, AtomicLogic> query)
        where Query : IDataQuery
    {
        dataQuery.AddLogic(query(dataQuery.GetCompareField(columnName)));
        return dataQuery;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query dataQuery, Func<SqlQuery, SqlQuery> query)
        where Query : IDataQuery
    {
        dataQuery.ApplyQuery(query);
        return dataQuery;
    }
    #endregion
    #region 子查询逻辑
    /// <summary>
    /// EXISTS子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Query Exists<Query>(this Query dataQuery, ITableView source)
        where Query : IDataQuery
    {
        dataQuery.AddLogic(source.AsExists());
        return dataQuery;
    }
    /// <summary>
    /// NOT EXISTS子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Query NotExists<Query>(this Query dataQuery, ITableView source)
        where Query : IDataQuery
    {
        dataQuery.AddLogic(source.AsNotExists());
        return dataQuery;
    }    
    #endregion

    #region Column/ColumnValue
    /// <summary>
    /// 对列进行参数化查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Id]=@LastId
    /// <code>
    /// var q = new Query()
    ///     .Column("Id", "=" , "LastId");
    /// </code>
    /// </example>
    public static TQuery ColumnParameter<TQuery>(this TQuery query, string columnName, string op = "=", string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CheckParameter(query, columnName, CompareSymbol.Get(op), parameter));
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
    /// <example>
    /// [Id]>100
    /// <code>
    /// var q = new Query()
    ///     .ColumnValue("Id", 100, ">");
    /// </code>
    /// </example>
    public static TQuery ColumnValue<TQuery, TValue>(this TQuery query, string columnName, TValue value, string op = "=")
        where TQuery : IDataQuery
    {
        query.AddLogic(CheckValue(query, columnName, CompareSymbol.Get(op), value));
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
    /// <example>
    /// [Id]=@ParentId
    /// <code>
    /// var q = new Query()
    ///     .Equal("Id", "ParentId");
    /// </code>
    /// </example>
    public static TQuery ColumnEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Equal, parameter));
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
    /// <example>
    /// [Id]=100
    /// <code>
    /// var q = new Query()
    ///     .EqualValue("Id", 100);
    /// </code>
    /// </example>
    public static TQuery ColumnEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Equal, SqlValue.From(value)));
        return query;
        //return new CompareLogic(column, Compare.Equal, SqlValue.From(value));
    }
    /// <summary>
    /// 列不等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Status]&lt;>@FailStatus
    /// <code>
    /// var q = new Query()
    ///     .NotEqual("Status", "FailStatus");
    /// </code>
    /// </example>
    public static TQuery ColumnNotEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.NotEqual, parameter));
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
    /// <example>
    /// [Status]&lt;>0
    /// <code>
    /// var q = new Query()
    ///     .NotEqualValue("Status", false);
    /// </code>
    /// </example>
    public static TQuery ColumnNotEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.NotEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Score]>@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .Greater("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery ColumnGreater<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Greater, parameter));
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
    /// <example>
    /// [Score]>60
    /// <code>
    /// var q = new Query()
    ///     .GreaterValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnGreaterValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Greater, SqlValue.From(value)));
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
    /// <example>
    /// [Score]&lt;@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .Less("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery ColumnLess<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Less, parameter));
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
    /// <example>
    /// [Score]&lt;60
    /// <code>
    /// var q = new Query()
    ///     .LessValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnLessValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Less, SqlValue.From(value)));
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
    /// <example>
    /// [Score]>=@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .GreaterOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery ColumnGreaterEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.GreaterEqual, parameter));
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
    /// <example>
    /// [Score]>=60
    /// <code>
    /// var q = new Query()
    ///     .GreaterOrValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnGreaterEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.GreaterEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Score]&lt;=@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .LessOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery ColumnLessEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.LessEqual, parameter));
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
    /// <example>
    /// [Score]&lt;=60
    /// <code>
    /// var q = new Query()
    ///     .LessOrEqualValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnLessEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.LessEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Id] IN @Ids
    /// <code>
    /// var q = new Query()
    ///     .In("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery ColumnIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.In, parameter));
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
    /// <example>
    /// [Id] IN (1,3,5)
    /// <code>
    /// var q = new Query()
    ///     .InValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery ColumnInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.In, SqlValue.Values(values)));
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
    /// <example>
    /// [Id] NOT IN @Ids
    /// <code>
    /// var q = new Query()
    ///     .NotIn("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery ColumnNotIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.NotIn, parameter));
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
    /// <example>
    /// [Id] NOT IN (1,3,5)
    /// <code>
    /// var q = new Query()
    ///     .NotInValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery ColumnNotInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.NotIn, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 列是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <example>
    /// [Score] IS NULL
    /// <code>
    /// var q = new Query()
    ///     .IsNull("Score");
    /// </code>
    /// </example>
    public static TQuery ColumnIsNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataQuery
    {
        query.AddLogic(new IsNullLogic(query.GetCompareField(columnName)));
        return query;
    }
    /// <summary>
    /// 列不是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <example>
    /// [Score] IS NOT NULL
    /// <code>
    /// var q = new Query()
    ///     .NotNull("Score");
    /// </code>
    /// </example>
    public static TQuery ColumnNotNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataQuery
    {
        query.AddLogic(new IsNullLogic(query.GetCompareField(columnName)));
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
    /// <example>
    /// [Title] LIKE @KeyWord
    /// <code>
    /// var q = new Query()
    ///     .Like("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery ColumnLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.Like, parameter));
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
    /// <example>
    /// [Name] LIKE '张%'
    /// <code>
    /// var q = new Query()
    ///     .LikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery ColumnLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Like, SqlValue.From(value)));
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
    /// <example>
    /// [Title] NOT LIKE @KeyWord
    /// <code>
    /// var q = new Query()
    ///     .NotLike("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery ColumnNotLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataQuery
    {
        query.AddLogic(CreateCompareLogic(query, columnName, CompareSymbol.NotLike, parameter));
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
    /// <example>
    /// [Name] NOT LIKE '张%'
    /// <code>
    /// var q = new Query()
    ///     .NotLikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery ColumnNotLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataQuery
    {
        query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.NotLike, SqlValue.From(value)));
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
    /// <example>
    /// [Id] BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new Query()
    ///     .Between("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery ColumnBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName2(end, begin);
        query.AddLogic(new BetweenLogic(query.GetCompareField(columnName), Parameter.Use(begin), Parameter.Use(end)));
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
    /// <example>
    /// [Id] BETWEEN 11 AND 19
    /// <code>
    /// var q = new Query()
    ///     .Between("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery ColumnBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataQuery
    {
        query.AddLogic(new BetweenLogic(query.GetCompareField(columnName), SqlValue.From(begin), SqlValue.From(end)));
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
    /// <example>
    /// [Id] NOT BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new Query()
    ///     .NotBetween("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery ColumnNotBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName(end, begin);
        query.AddLogic(new NotBetweenLogic(query.GetCompareField(columnName), Parameter.Use(begin), Parameter.Use(end)));
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
    /// <example>
    /// [Id] NOT BETWEEN 11 AND 19
    /// <code>
    /// var q = new Query()
    ///     .NotBetweenValue("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery ColumnNotBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataQuery
    {
        query.AddLogic(new NotBetweenLogic(query.GetCompareField(columnName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    #endregion

    private static AtomicLogic CheckValue<TValue>(IDataQuery query, string columnName, CompareSymbol op, TValue value)
    {
        var field = query.GetCompareField(columnName);
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
    private static AtomicLogic CheckParameter(IDataQuery query, string columnName, CompareSymbol op, string parameter)
    {  
        if (op == CompareSymbol.IsNull)
            return new IsNullLogic(query.GetCompareField(columnName));
        else if (op == CompareSymbol.NotNull)
            return new NotNullLogic(query.GetCompareField(columnName));
        return CreateCompareLogic(query, columnName, op, parameter);
    }
    private static AtomicLogic CreateCompareLogic(IDataQuery query, string columnName, CompareSymbol op, string parameter)
    {
        var field = query.GetCompareField(columnName);
        parameter = Parameter.CheckName(parameter, columnName);
        return new CompareLogic(field, op, Parameter.Use(parameter));
    }
}
