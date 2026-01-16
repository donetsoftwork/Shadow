using Hand.Data;
using ShadowSql.Engines;

namespace Dapper.Shadow;

/// <summary>
/// 调用执行器
/// </summary>
public static partial class DapperShadowServices
{
    /// <summary>
    /// 构造执行器
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="source">数据库连接</param>
    /// <param name="buffered"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static DapperExecutor Use(this ISqlEngine engine, ISqlSource source, bool buffered = true, int capacity = 128)
        => new(engine, source, buffered, capacity);
}
