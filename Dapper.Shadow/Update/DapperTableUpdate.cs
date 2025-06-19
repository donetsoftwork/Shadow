using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Update;
using System;

namespace Dapper.Shadow.Update;

/// <summary>
/// 修改表
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class DapperTableUpdate<TTable>(IExecutor executor, TTable table, ISqlLogic filter)
    : TableUpdate<TTable>(table, filter), IDapperExecute
    where TTable : IUpdateTable
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
    /// 添加修改信息
    /// </summary>
    /// <param name="operation">操作</param>
    /// <returns></returns>
    new public DapperTableUpdate<TTable> Set(Func<TTable, IAssignInfo> operation)
    {
        SetCore(operation(_source));
        return this;
    }
}
