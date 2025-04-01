﻿using ShadowSql.Generators;
using ShadowSql.Join;
using ShadowSql.Queries;

namespace Dapper.Shadow.Join;

/// <summary>
/// 多表查询
/// </summary>
/// <param name="executor"></param>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public class DapperMultiTableQuery(IExecutor executor, IIdentifierGenerator aliasGenerator, SqlQuery filter)
    : MultiTableQuery(aliasGenerator, filter), IDapperSource
{
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
}
