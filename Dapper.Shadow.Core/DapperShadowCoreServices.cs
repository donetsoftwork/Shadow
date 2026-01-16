using Hand.Data;
using ShadowSql.Engines;

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
    /// <param name="source">数据库连接</param>
    /// <param name="engine">数据库引擎</param>
    /// <param name="tableName">表名</param>
    /// <param name="buffered"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static DapperTable From(this ISqlSource source, ISqlEngine engine, string tableName, bool buffered = true, int capacity = 128)
        => new DapperExecutor(engine, source, buffered, capacity)
            .From(tableName);
}
