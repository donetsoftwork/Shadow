using ShadowSql.Identifiers;

namespace Dapper.Shadow;

/// <summary>
/// Dapper数据源
/// </summary>
public interface IDapperSource : ITableView
{
    /// <summary>
    /// 执行器
    /// </summary>
    IExecutor Executor { get; }
}
/// <summary>
/// Dapper表
/// </summary>
public interface IDapperTable : ITable, IDapperSource
{
}
