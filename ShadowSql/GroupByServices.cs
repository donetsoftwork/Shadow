using ShadowSql.AliasTables;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Variants;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// 分组扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region GroupBy
    #region GroupByTableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByTableQuery<TTable>(table, EmptyLogic.Instance, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByTableQuery<TTable>(table, query._filter, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, ISqlLogic where, IFieldView[] fields)
        where TTable : ITable
        => new(table, where, fields);
    #endregion
    #region GroupByAliasTableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this TableAlias<TTable> table, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByAliasTableQuery<TTable>(table, EmptyLogic.Instance, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this AliasTableQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByAliasTableQuery<TTable>(table, query._filter, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this TableAlias<TTable> table, ISqlLogic where, IFieldView[] fields)
        where TTable : ITable
        => new(table, where, fields);
    #endregion
    #region GroupByMultiQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy(this IMultiView multiTable, params IEnumerable<string> columnNames)
    {
        var columns = multiTable.SelectFields(columnNames)
            .ToArray();
        return new GroupByMultiQuery(multiTable, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy(this IMultiView multiTable, IFieldView[] fields)
        => new(multiTable, fields);
    #endregion
    #endregion
    #region SqlGroupBy
    #region GroupByTableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByTableSqlQuery<TTable>(table, EmptyLogic.Instance, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> GroupBy<TTable>(this TableSqlQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByTableSqlQuery<TTable>(table, query._filter, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, ISqlLogic where, params IFieldView[] fields)
        where TTable : ITable
        => new(table, where, fields);
    #endregion
    #region GroupByAliasTableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableAlias<TTable> table, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByAliasTableSqlQuery<TTable>(table, EmptyLogic.Instance, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> GroupBy<TTable>(this AliasTableSqlQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByAliasTableSqlQuery<TTable>(table, query._filter, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableAlias<TTable> table, ISqlLogic where, params IFieldView[] fields)
        where TTable : ITable
        => new(table, where, fields);
    #endregion
    #endregion
}
