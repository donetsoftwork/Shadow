using ShadowSql.Engines;
using System.Data;

namespace Dapper.Shadow;

/// <summary>
/// Dapper扩展方法
/// </summary>
public static partial class DapperShadowCoreServices
{
    /// <summary>
    /// 构造Dapper表
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static DapperTable From(this IExecutor executor, string tableName)
        => new(executor, tableName);
    /// <summary>
    /// 构造Dapper表
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="engine">数据库引擎</param>
    /// <param name="tableName">表名</param>
    /// <param name="buffered"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static DapperTable From(this IDbConnection connection, ISqlEngine engine, string tableName, bool buffered = true, int capacity = 128)
        => new DapperExecutor(engine, connection, buffered, capacity)
            .From(tableName);
}
