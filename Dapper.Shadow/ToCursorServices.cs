using Dapper.Shadow.Cursors;
using Dapper.Shadow.GroupBy;
using Dapper.Shadow.Join;

namespace Dapper.Shadow;

/// <summary>
/// 构造范围筛选扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    /// <summary>
    /// 多表范围筛选
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static DapperMultiTableCursor ToCursor(this DapperMultiTableSqlQuery multiTable, int limit = 0, int offset = 0)
        => new(multiTable.Executor, multiTable, limit, offset);
    /// <summary>
    /// 多表范围筛选
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static DapperMultiTableCursor Take(this DapperMultiTableSqlQuery multiTable, int limit, int offset = 0)
        => new(multiTable.Executor, multiTable, limit, offset);
    /// <summary>
    /// 联表范围筛选
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static DapperMultiTableCursor ToCursor(this DapperJoinTableSqlQuery joinTable, int limit = 0, int offset = 0)
        => new(joinTable.Executor, joinTable, limit, offset);
    /// <summary>
    /// 联表范围筛选
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static DapperMultiTableCursor Take(this DapperJoinTableSqlQuery joinTable, int limit, int offset = 0)
        => new(joinTable.Executor, joinTable, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static DapperGroupByMultiCursor ToCursor(this DapperGroupByMultiSqlQuery groupBy, int limit = 0, int offset = 0)
        => new(groupBy.Executor, groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static DapperGroupByMultiCursor Take(this DapperGroupByMultiSqlQuery groupBy, int limit, int offset = 0)
        => new(groupBy.Executor, groupBy, limit, offset);
}
