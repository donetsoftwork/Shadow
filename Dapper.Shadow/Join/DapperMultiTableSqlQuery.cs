using ShadowSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Queries;
using System;

namespace Dapper.Shadow.Join;

/// <summary>
/// 多表查询
/// </summary>
/// <param name="executor"></param>
/// <param name="filter"></param>
public class DapperMultiTableSqlQuery(IExecutor executor, SqlQuery filter)
    : MultiTableSqlQuery(filter), IDapperSource
{
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    new public DapperMultiTableSqlQuery Apply<TAliasTable>(string tableName, Func<SqlQuery, TAliasTable, SqlQuery> query)
        where TAliasTable : IAliasTable
    {
        _filter = query(_filter, this.From<TAliasTable>(tableName));
        return this;
    }
}
