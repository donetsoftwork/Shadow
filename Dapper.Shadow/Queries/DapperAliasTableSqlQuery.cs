using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace Dapper.Shadow.Queries;

/// <summary>
/// Dapper查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="aliasTable">别名表</param>
/// <param name="query">查询</param>
public class DapperAliasTableSqlQuery<TTable>(IExecutor executor, IAliasTable<TTable> aliasTable, SqlQuery query)
    : AliasTableSqlQuery<TTable>(aliasTable, query), IDapperSource
    where TTable : ITable
{
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
    #region 查询扩展
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public DapperAliasTableSqlQuery<TTable> Where(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter.AddLogic(query(Prefix(select)));
        return this;
    }
    #endregion
}
