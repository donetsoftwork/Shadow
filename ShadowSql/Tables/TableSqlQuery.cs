using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Tables;

/// <summary>
/// sql查询表
/// </summary>
/// <param name="table"></param>
/// <param name="query"></param>
public class TableSqlQuery<TTable>(TTable table, SqlQuery query)   
    : DataSqlQuery<TTable>(table, query), ITableView, IWhere
     where TTable : ITable
{
    #region 扩展查询功能
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableSqlQuery<TTable> Where(Func<TTable, AtomicLogic> query)
    {
        AddLogic(query(_source));
        return this;
    }
    #endregion
}