using ShadowSql.Engines;
using System.Data;

namespace Dapper.Shadow;

/// <summary>
/// Dapper扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    /// <summary>
    /// 构造Dapper表
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static DapperTable From(this IExecutor executor, string tableName)
        => new(executor, tableName);
    /// <summary>
    /// 构造Dapper表
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="engine"></param>
    /// <param name="tableName"></param>
    /// <param name="buffered"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static DapperTable From(this IDbConnection connection, ISqlEngine engine, string tableName, bool buffered = true, int capacity = 128)
        => new DapperExecutor(engine, connection, buffered, capacity)
            .From(tableName);
}
