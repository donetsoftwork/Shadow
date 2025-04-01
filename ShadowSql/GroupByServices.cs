using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// 分组扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region GroupBy
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, params string[] columnNames)
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
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, params string[] columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByTableQuery<TTable>(table, query.Filter, columns);
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
    #region SqlGroupBy
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, params string[] columnNames)
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
    public static GroupByTableSqlQuery<TTable> GroupBy<TTable>(this TableSqlQuery<TTable> query, params string[] columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        var columns = table.SelectFields(columnNames)
            .ToArray();
        return new GroupByTableSqlQuery<TTable>(table, query.Filter, columns);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, ISqlLogic where, IFieldView[] fields)
        where TTable : ITable
        => new(table, where, fields);
    #endregion
}
