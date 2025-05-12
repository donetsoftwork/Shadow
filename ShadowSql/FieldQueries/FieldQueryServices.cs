using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.SqlVales;
using System;
using System.Collections.Generic;

namespace ShadowSql.FieldQueries;

/// <summary>
/// 按字段查询服务
/// </summary>
public static partial class FieldQueryServices
{
    #region FieldParameter/FieldValue
    /// <summary>
    /// 对字段进行参数化查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Id]=@LastId
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldParameter("Id", "=" , "LastId");
    /// </code>
    /// </example>
    public static TQuery FieldParameter<TQuery>(this TQuery query, string fieldName, string op = "=", string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CheckParameter(query, fieldName, CompareSymbol.Get(op), parameter));
        return query;
    }
    /// <summary>
    /// 对字段按值查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <param name="op"></param>
    /// <returns></returns>
    /// <example>
    /// [Id]>100
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldValue("Id", 100, ">");
    /// </code>
    /// </example>
    public static TQuery FieldValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value, string op = "=")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CheckValue(query, fieldName, CompareSymbol.Get(op), value));
        return query;
    }
    /// <summary>
    /// 字段等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Id]=@ParentId
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldEqual("Id", "ParentId");
    /// </code>
    /// </example>
    public static TQuery FieldEqual<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.Equal, parameter));
        return query;
    }
    /// <summary>
    /// 字段等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Id]=100
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldEqualValue("Id", 100);
    /// </code>
    /// </example>
    public static TQuery FieldEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.Equal, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段不等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Status]&lt;>@FailStatus
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotEqual("Status", "FailStatus");
    /// </code>
    /// </example>
    public static TQuery FieldNotEqual<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.NotEqual, parameter));
        return query;
    }
    /// <summary>
    /// 字段不等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Status]&lt;>0
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotEqualValue("Status", false);
    /// </code>
    /// </example>
    public static TQuery FieldNotEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.NotEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段大于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]>@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldGreater("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery FieldGreater<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.Greater, parameter));
        return query;
    }
    /// <summary>
    /// 字段大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]>60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldGreaterValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery FieldGreaterValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.Greater, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段小于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]&lt;@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldLess("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery FieldLess<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.Less, parameter));
        return query;
    }
    /// <summary>
    /// 字段小于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]&lt;60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldLessValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery FieldLessValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.Less, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段大于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]>=@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldGreaterOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery FieldGreaterEqual<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.GreaterEqual, parameter));
        return query;
    }
    /// <summary>
    /// 字段大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]>=60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldGreaterOrValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery FieldGreaterEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.GreaterEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段小于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]&lt;=@AvgScore
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldLessOrEqual("Score", "AvgScore");
    /// </code>
    /// </example>
    public static TQuery FieldLessEqual<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.LessEqual, parameter));
        return query;
    }
    /// <summary>
    /// 字段小于等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Score]&lt;=60
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldLessOrEqualValue("Score", 60);
    /// </code>
    /// </example>
    public static TQuery FieldLessEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.LessEqual, SqlValue.From(value)));
        return query;
    }

    /// <summary>
    /// 字段包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] IN @Ids
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldIn("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery FieldIn<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.In, parameter));
        return query;
    }
    /// <summary>
    /// 字段包含于值字段表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] IN (1,3,5)
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldInValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery FieldInValue<TQuery, TValue>(this TQuery query, string fieldName, params IEnumerable<TValue> values)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.In, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 字段不包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] NOT IN @Ids
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotIn("Id", "Ids");
    /// </code>
    /// </example>
    public static TQuery FieldNotIn<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.NotIn, parameter));
        return query;
    }
    /// <summary>
    /// 字段不包含于值字段表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] NOT IN (1,3,5)
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotInValue("Id", 1, 3, 5);
    /// </code>
    /// </example>
    public static TQuery FieldNotInValue<TQuery, TValue>(this TQuery query, string fieldName, params IEnumerable<TValue> values)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.NotIn, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 字段是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    /// <example>
    /// [Score] IS NULL
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldIsNull("Score");
    /// </code>
    /// </example>
    public static TQuery FieldIsNull<TQuery>(this TQuery query, string fieldName)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.GetCompareField(fieldName)));
        return query;
    }
    /// <summary>
    /// 字段不是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    /// <example>
    /// [Score] IS NOT NULL
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotNull("Score");
    /// </code>
    /// </example>
    public static TQuery FieldNotNull<TQuery>(this TQuery query, string fieldName)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.GetCompareField(fieldName)));
        return query;
    }
    /// <summary>
    /// 字段匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Title] LIKE @KeyWord
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldLike("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery FieldLike<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.Like, parameter));
        return query;
    }
    /// <summary>
    /// 字段匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Name] LIKE '张%'
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldLikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery FieldLikeValue<TQuery>(this TQuery query, string fieldName, string value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.Like, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段不匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    /// <example>
    /// [Title] NOT LIKE @KeyWord
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotLike("Title", "KeyWord");
    /// </code>
    /// </example>
    public static TQuery FieldNotLike<TQuery>(this TQuery query, string fieldName, string parameter = "")
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query, fieldName, CompareSymbol.NotLike, parameter));
        return query;
    }
    /// <summary>
    /// 字段不匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <example>
    /// [Name] NOT LIKE '张%'
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotLikeValue("Name", "张%");
    /// </code>
    /// </example>
    public static TQuery FieldNotLikeValue<TQuery>(this TQuery query, string fieldName, string value)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.GetCompareField(fieldName), CompareSymbol.NotLike, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldBetween("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery FieldBetween<TQuery>(this TQuery query, string fieldName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, fieldName);
        end = Parameter.CheckName2(end, begin);
        query.Query.AddLogic(new BetweenLogic(query.GetCompareField(fieldName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 字段在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] BETWEEN 11 AND 19
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldBetween("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery FieldBetweenValue<TQuery, TValue>(this TQuery query, string fieldName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new BetweenLogic(query.GetCompareField(fieldName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    /// <summary>
    /// 字段不在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] NOT BETWEEN @IdBegin AND @IdEnd
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotBetween("Id", "IdBegin", "IdEnd");
    /// </code>
    /// </example>
    public static TQuery FieldNotBetween<TQuery>(this TQuery query, string fieldName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, fieldName);
        end = Parameter.CheckName(end, begin);
        query.Query.AddLogic(new NotBetweenLogic(query.GetCompareField(fieldName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 字段不在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <example>
    /// [Id] NOT BETWEEN 11 AND 19
    /// <code>
    /// var q = new TableSqlQuery("Users")
    ///     .FieldNotBetweenValue("Id", 11, 19);
    /// </code>
    /// </example>
    public static TQuery FieldNotBetweenValue<TQuery, TValue>(this TQuery query, string fieldName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery
    {
        query.Query.AddLogic(new NotBetweenLogic(query.GetCompareField(fieldName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    #endregion
    #region TableField
    /// <summary>
    /// 按字段查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static Query TableField<Query>(this Query query, string tableName, string fieldName, Func<ICompareField, AtomicLogic> compare)
        where Query : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(compare(query.From(tableName).GetCompareField(fieldName)));
        return query;
    }
    #endregion
    #region TableFieldParameter/TableFieldValue
    /// <summary>
    /// 对字段进行参数化查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldParameter<TQuery>(this TQuery query, string tableName, string fieldName, string op = "=", string parameter = "")
        where TQuery : IMultiView, IDataSqlQuery
    {
        query.Query.AddLogic(CheckParameter(query.From(tableName), fieldName, CompareSymbol.Get(op), parameter));
        return query;
    }
    /// <summary>
    /// 对字段按值查询
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <param name="op"></param>
    /// <returns></returns>
    public static TQuery TableFieldValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value, string op = "=")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CheckValue(query.From(tableName), fieldName, CompareSymbol.Get(op), value));
        return query;
    }
    /// <summary>
    /// 字段等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.Equal, parameter));
        return query;
    }
    /// <summary>
    /// 字段等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.Equal, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段不等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.NotEqual, parameter));
        return query;
    }
    /// <summary>
    /// 字段不等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.NotEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段大于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldGreater<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.Greater, parameter));
        return query;
    }
    /// <summary>
    /// 字段大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldGreaterValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.Greater, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段小于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldLess<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.Less, parameter));
        return query;
    }
    /// <summary>
    /// 字段小于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldLessValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.Less, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段大于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldGreaterEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.GreaterEqual, parameter));
        return query;
    }
    /// <summary>
    /// 字段大于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldGreaterEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.GreaterEqual, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段小于等于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldLessEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.LessEqual, parameter));
        return query;
    }
    /// <summary>
    /// 字段小于等于值
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldLessEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.LessEqual, SqlValue.From(value)));
        return query;
    }

    /// <summary>
    /// 字段包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldIn<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.In, parameter));
        return query;
    }
    /// <summary>
    /// 字段包含于值字段表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static TQuery TableFieldInValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, params IEnumerable<TValue> values)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.In, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 字段不包含于参数
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotIn<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.NotIn, parameter));
        return query;
    }
    /// <summary>
    /// 字段不包含于值字段表
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotInValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, params IEnumerable<TValue> values)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.NotIn, SqlValue.Values(values)));
        return query;
    }
    /// <summary>
    /// 字段是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static TQuery TableFieldIsNull<TQuery>(this TQuery query, string tableName, string fieldName)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.From(tableName).GetCompareField(fieldName)));
        return query;
    }
    /// <summary>
    /// 字段不是null
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotNull<TQuery>(this TQuery query, string tableName, string fieldName)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new IsNullLogic(query.From(tableName).GetCompareField(fieldName)));
        return query;
    }
    /// <summary>
    /// 字段匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldLike<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.Like, parameter));
        return query;
    }
    /// <summary>
    /// 字段匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldLikeValue<TQuery>(this TQuery query, string tableName, string fieldName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.Like, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段不匹配参数模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotLike<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(CreateCompareLogic(query.From(tableName), fieldName, CompareSymbol.NotLike, parameter));
        return query;
    }
    /// <summary>
    /// 字段不匹配值模式
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotLikeValue<TQuery>(this TQuery query, string tableName, string fieldName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new CompareLogic(query.From(tableName).GetCompareField(fieldName), CompareSymbol.NotLike, SqlValue.From(value)));
        return query;
    }
    /// <summary>
    /// 字段在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static TQuery TableFieldBetween<TQuery>(this TQuery query, string tableName, string fieldName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, fieldName);
        end = Parameter.CheckName2(end, begin);
        query.Query.AddLogic(new BetweenLogic(query.From(tableName).GetCompareField(fieldName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 字段在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static TQuery TableFieldBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new BetweenLogic(query.From(tableName).GetCompareField(fieldName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    /// <summary>
    /// 字段不在两参数之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotBetween<TQuery>(this TQuery query, string tableName, string fieldName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        begin = Parameter.CheckName(begin, fieldName);
        end = Parameter.CheckName(end, begin);
        query.Query.AddLogic(new NotBetweenLogic(query.From(tableName).GetCompareField(fieldName), Parameter.Use(begin), Parameter.Use(end)));
        return query;
    }
    /// <summary>
    /// 字段不在两值之前
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="query"></param>
    /// <param name="tableName"></param>
    /// <param name="fieldName"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static TQuery TableFieldNotBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery
    {
        query.Query.AddLogic(new NotBetweenLogic(query.From(tableName).GetCompareField(fieldName), SqlValue.From(begin), SqlValue.From(end)));
        return query;
    }
    #endregion
    private static AtomicLogic CheckValue<TValue>(ITableView view, string fieldName, CompareSymbol op, TValue value)
    {
        var field = view.GetCompareField(fieldName);
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
    private static AtomicLogic CheckParameter(ITableView view, string fieldName, CompareSymbol op, string parameter)
    {
        if (op == CompareSymbol.IsNull)
            return new IsNullLogic(view.GetCompareField(fieldName));
        else if (op == CompareSymbol.NotNull)
            return new NotNullLogic(view.GetCompareField(fieldName));
        return CreateCompareLogic(view, fieldName, op, parameter);
    }
    private static CompareLogic CreateCompareLogic(ITableView view, string fieldName, CompareSymbol op, string parameter)
    {
        var field = view.GetCompareField(fieldName);
        parameter = Parameter.CheckName(parameter, fieldName);
        return new CompareLogic(field, op, Parameter.Use(parameter));
    }
}
