using ShadowSql.Engines;
using System.Data;

namespace Dapper.Shadow;

/// <summary>
/// 调用执行器
/// </summary>
public static partial class DapperShadowServices
{
    /// <summary>
    /// 构造执行器
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="connection"></param>
    /// <param name="buffered"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static DapperExecutor Use(this ISqlEngine engine, IDbConnection connection, bool buffered = true, int capacity = 128)
        => new(engine, connection, buffered, capacity);
}
