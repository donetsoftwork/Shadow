using Dapper.Shadow.Insert;
using ShadowSql.Select;

namespace Dapper.Shadow;

/// <summary>
/// 构造插入扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region SingleInsert
    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperSingleInsert<TTable> ToDapperInsert<TTable>(this TTable table)
        where TTable : IDapperTable
        => new(table.Executor, table);
    #endregion
    #region MultiInsert
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperMultiInsert<TTable> ToDapperMultiInsert<TTable>(this TTable table)
        where TTable : IDapperTable
        => new(table.Executor, table);
    #endregion
    #region SelectInsert
    /// <summary>
    /// 插入Select
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static DapperSelectInsert<TTable> ToDapperInsert<TTable>(this TTable table, ISelect select)
        where TTable : IDapperTable
        => new(table.Executor, table, select);
    /// <summary>
    /// 插入Select
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="select"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperSelectInsert<TTable> DapperInsertTo<TTable>(this ISelect select, TTable table)
      where TTable : IDapperTable
      => new(table.Executor, table, select);
    #endregion
}
