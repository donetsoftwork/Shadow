using ShadowSql.Fragments;
using ShadowSql.Select;
using ShadowSql.SingleSelect;

namespace Dapper.Shadow;

/// <summary>
/// Dapper执行
/// </summary>
public interface IDapperExecute : IExecuteSql
{
    /// <summary>
    /// 执行器
    /// </summary>
    IExecutor Executor { get; }
}

/// <summary>
/// Dapper筛选列
/// </summary>
public interface IDapperSelect : ISelect
{
    /// <summary>
    /// 执行器
    /// </summary>
    IExecutor Executor { get; }
}
/// <summary>
/// Dapper筛选单列
/// </summary>
public interface IDapperSingleSelect : IDapperSelect, ISingleSelect;
