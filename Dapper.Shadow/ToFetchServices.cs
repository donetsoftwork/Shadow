﻿using Dapper.Shadow.Fetches;
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
    /// <param name="multiTable"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static DapperMultiTableFetch ToFetch(this DapperMultiTableQuery multiTable, int limit = 0, int offset = 0)
        => new(multiTable.Executor, multiTable, limit, offset);
    /// <summary>
    /// 联表范围筛选
    /// </summary>
    /// <param name="joinTable"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static DapperMultiTableFetch ToFetch(this DapperJoinTableQuery joinTable, int limit = 0, int offset = 0)
        => new(joinTable.Executor, joinTable, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static DapperGroupByMultiFetch ToFetch(this DapperGroupByMultiQuery groupBy, int limit = 0, int offset = 0)
        => new(groupBy.Executor, groupBy, limit, offset);
}
