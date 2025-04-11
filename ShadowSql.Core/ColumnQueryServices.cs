using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.SqlVales;
using System;

namespace ShadowSql;

public static partial class ShadowSqlCoreServices
{
    #region Column
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static Query Column<Query>(this Query query, string columnName, Func<ICompareField, AtomicLogic> compare)
        where Query : IDataSqlQuery
    {
        query.Query.AddLogic(compare(query.GetCompareField(columnName)));
        return query;
    }
    #endregion
    #region ColumnParameter/ColumnValue
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
    /// <example>
    /// [Id]>100
    /// <code>
    /// var q = new Query()
    ///     .ColumnValue("Id", 100, ">");
    /// </code>
    /// </example>
    public static TQuery ColumnValue<TQuery, TValue>(this TQuery query, string columnName, TValue value, string op = "=")
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
    /// <example>
    /// [Id]=@ParentId
    /// <code>
    /// var q = new Query()
    ///     .Equal("Id", "ParentId");
    /// </code>
    /// </example>
    public static TQuery ColumnEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
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
    /// <example>
    /// [Id]=100
    /// <code>
    /// var q = new Query()
    ///     .EqualValue("Id", 100);
    /// </code>
    /// </example>
    public static TQuery ColumnEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Equal, SqlValue.From(value)));
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
    /// <example>
    /// [Status]&lt;>@FailStatus
    /// <code>
    /// var q = new Query()
    ///     .NotEqual("Status", "FailStatus");
    /// </code>
    /// </example>
    public static TQuery ColumnNotEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
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
    /// <example>
    /// [Status]&lt;>0
    /// <code>
    /// var q = new Query()
    ///     .NotEqualValue("Status", false);
    /// </code>
    /// </example>
    public static TQuery ColumnNotEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.NotEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Score]>60
    /// <code>
    /// var q = new Query()
    ///     .GreaterValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnGreaterValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Greater, SqlValue.From(value)));
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
    /// <example>
    /// [Score]&lt;60
    /// <code>
    /// var q = new Query()
    ///     .LessValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnLessValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Less, SqlValue.From(value)));
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
    /// <example>
    /// [Score]>=60
    /// <code>
    /// var q = new Query()
    ///     .GreaterOrValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnGreaterEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.GreaterEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Score]&lt;=60
    /// <code>
    /// var q = new Query()
    ///     .LessOrEqualValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery ColumnLessEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.LessEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Id] IN (1,3,5)
    /// <code>
    /// var q = new Query()
    ///     .InValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery ColumnInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.In, SqlValue.Values(values)));
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
    /// <example>
    /// [Id] NOT IN (1,3,5)
    /// <code>
    /// var q = new Query()
    ///     .NotInValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery ColumnNotInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.NotIn, SqlValue.Values(values)));
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
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.GetCompareField(columnName)));
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
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.GetCompareField(columnName)));
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
    /// <example>
    /// [Name] LIKE '张%'
    /// <code>
    /// var q = new Query()
    ///     .LikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery ColumnLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.Like, SqlValue.From(value)));
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
    /// <example>
    /// [Name] NOT LIKE '张%'
    /// <code>
    /// var q = new Query()
    ///     .NotLikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery ColumnNotLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(columnName), CompareSymbol.NotLike, SqlValue.From(value)));
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
        where TQuery : IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName2(end, begin);
        query.Query.AddLogic(new BetweenLogic(query.GetCompareField(columnName), Parameter.Use(begin), Parameter.Use(end)));
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
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new BetweenLogic(query.GetCompareField(columnName), SqlValue.From(begin), SqlValue.From(end)));
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
        where TQuery : IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName(end, begin);
        query.Query.AddLogic(new NotBetweenLogic(query.GetCompareField(columnName), Parameter.Use(begin), Parameter.Use(end)));
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
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new NotBetweenLogic(query.GetCompareField(columnName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    #endregion
    #region TableColumn
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="columnName"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static Query TableColumn<Query>(this Query query, string tableName, string columnName, Func<ICompareField, AtomicLogic> compare)
        where Query : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(compare(query.From(tableName).GetCompareField(columnName)));
        return query;
    }
    #endregion
    #region TableColumnParameter/TableColumnValue
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
    /// <example>
    /// [Id]=@LastId
    /// <code>
    /// var q = new Query()
    ///     .Column("Id", "=" , "LastId");
    /// </code>
    /// </example>
    public static TQuery TableColumnParameter<TQuery>(this TQuery query, string tableName, string columnName, string op = "=", string parameter = "")
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
    /// <example>
    /// [Id]>100
    /// <code>
    /// var q = new Query()
    ///     .ColumnValue("Id", 100, ">");
    /// </code>
    /// </example>
    public static TQuery TableColumnValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value, string op = "=")
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
    /// <example>
    /// [Id]=@ParentId
    /// <code>
    /// var q = new Query()
    ///     .Equal("Id", "ParentId");
    /// </code>
    /// </example>
    public static TQuery TableColumnEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Id]=100
    /// <code>
    /// var q = new Query()
    ///     .EqualValue("Id", 100);
    /// </code>
    /// </example>
    public static TQuery TableColumnEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.Equal, SqlValue.From(value)));
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
    /// <example>
    /// [Status]&lt;>@FailStatus
    /// <code>
    /// var q = new Query()
    ///     .NotEqual("Status", "FailStatus");
    /// </code>
    /// </example>
    public static TQuery TableColumnNotEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Status]&lt;>0
    /// <code>
    /// var q = new Query()
    ///     .NotEqualValue("Status", false);
    /// </code>
    /// </example>
    public static TQuery TableColumnNotEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.NotEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Score]>@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .Greater("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery TableColumnGreater<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Score]>60
    /// <code>
    /// var q = new Query()
    ///     .GreaterValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery TableColumnGreaterValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.Greater, SqlValue.From(value)));
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
    /// <example>
    /// [Score]&lt;@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .Less("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery TableColumnLess<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Score]&lt;60
    /// <code>
    /// var q = new Query()
    ///     .LessValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery TableColumnLessValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.Less, SqlValue.From(value)));
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
    /// <example>
    /// [Score]>=@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .GreaterOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery TableColumnGreaterEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Score]>=60
    /// <code>
    /// var q = new Query()
    ///     .GreaterOrValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery TableColumnGreaterEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.GreaterEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Score]&lt;=@AvgScore
    /// <code>
    /// var q = new Query()
    ///     .LessOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery TableColumnLessEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Score]&lt;=60
    /// <code>
    /// var q = new Query()
    ///     .LessOrEqualValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery TableColumnLessEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.LessEqual, SqlValue.From(value)));
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
    /// <example>
    /// [Id] IN @Ids
    /// <code>
    /// var q = new Query()
    ///     .In("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery TableColumnIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Id] IN (1,3,5)
    /// <code>
    /// var q = new Query()
    ///     .InValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery TableColumnInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.In, SqlValue.Values(values)));
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
    /// <example>
    /// [Id] NOT IN @Ids
    /// <code>
    /// var q = new Query()
    ///     .NotIn("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery TableColumnNotIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Id] NOT IN (1,3,5)
    /// <code>
    /// var q = new Query()
    ///     .NotInValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery TableColumnNotInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.NotIn, SqlValue.Values(values)));
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
    /// <example>
    /// [Score] IS NULL
    /// <code>
    /// var q = new Query()
    ///     .IsNull("Score");
    /// </code>
    /// </example>
    public static TQuery TableColumnIsNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.From(tableName).GetCompareField(columnName)));
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
    /// <example>
    /// [Score] IS NOT NULL
    /// <code>
    /// var q = new Query()
    ///     .NotNull("Score");
    /// </code>
    /// </example>
    public static TQuery TableColumnNotNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.From(tableName).GetCompareField(columnName)));
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
    /// <example>
    /// [Title] LIKE @KeyWord
    /// <code>
    /// var q = new Query()
    ///     .Like("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery TableColumnLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Name] LIKE '张%'
    /// <code>
    /// var q = new Query()
    ///     .LikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery TableColumnLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.Like, SqlValue.From(value)));
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
    /// <example>
    /// [Title] NOT LIKE @KeyWord
    /// <code>
    /// var q = new Query()
    ///     .NotLike("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery TableColumnNotLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
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
    /// <example>
    /// [Name] NOT LIKE '张%'
    /// <code>
    /// var q = new Query()
    ///     .NotLikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery TableColumnNotLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(columnName), CompareSymbol.NotLike, SqlValue.From(value)));
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
    /// <example>
    /// [Id] BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new Query()
    ///     .Between("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery TableColumnBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName2(end, begin);
        query.Query.AddLogic(new BetweenLogic(query.From(tableName).GetCompareField(columnName), Parameter.Use(begin), Parameter.Use(end)));
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
    /// <example>
    /// [Id] BETWEEN 11 AND 19
    /// <code>
    /// var q = new Query()
    ///     .Between("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery TableColumnBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new BetweenLogic(query.From(tableName).GetCompareField(columnName), SqlValue.From(begin), SqlValue.From(end)));
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
    /// <example>
    /// [Id] NOT BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new Query()
    ///     .NotBetween("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery TableColumnNotBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, columnName);
        end = Parameter.CheckName(end, begin);
        query.Query.AddLogic(new NotBetweenLogic(query.From(tableName).GetCompareField(columnName), Parameter.Use(begin), Parameter.Use(end)));
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
    /// <example>
    /// [Id] NOT BETWEEN 11 AND 19
    /// <code>
    /// var q = new Query()
    ///     .NotBetweenValue("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery TableColumnNotBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new NotBetweenLogic(query.From(tableName).GetCompareField(columnName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    #endregion
    private static AtomicLogic CheckValue<TValue>(ITableView view, string columnName, CompareSymbol op, TValue value)
    {
        var field = view.GetCompareField(columnName);
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
    private static AtomicLogic CheckParameter(ITableView view, string columnName, CompareSymbol op, string parameter)
    {
        if (op == CompareSymbol.IsNull)
            return new IsNullLogic(view.GetCompareField(columnName));
        else if (op == CompareSymbol.NotNull)
            return new NotNullLogic(view.GetCompareField(columnName));
        return CreateCompareLogic(view, columnName, op, parameter);
    }
    private static AtomicLogic CreateCompareLogic(ITableView view, string columnName, CompareSymbol op, string parameter)
    {
        var field = view.GetCompareField(columnName);
        parameter = Parameter.CheckName(parameter, columnName);
        return new CompareLogic(field, op, Parameter.Use(parameter));
    }
}
