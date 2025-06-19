using ShadowSql.Identifiers;
using ShadowSql.Insert;
using System;

namespace Dapper.Shadow.Insert;

/// <summary>
/// 单条插入
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="table">表</param>
public class DapperSingleInsert<TTable>(IExecutor executor, TTable table)
    : SingleInsert<TTable>(table)
    , IDapperExecute
    where TTable : IInsertTable
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
    /// 增加插入值
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    new public DapperSingleInsert<TTable> Insert(Func<TTable, IInsertValue> select)
    {
        base.Insert(select);
        return this;
    }
}
