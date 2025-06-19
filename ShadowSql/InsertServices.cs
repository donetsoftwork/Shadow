using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
using ShadowSql.Tables;

namespace ShadowSql;

/// <summary>
/// 构造插入扩展方法
/// </summary>
public static partial class ShadowSqlServices
{    
    #region ToInsert
    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static SingleInsert<TTable> ToInsert<TTable>(this TTable table)
        where TTable : IInsertTable
        => new(table);
    #endregion
    #region ToMultiInsert
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static MultiInsert<TTable> ToMultiInsert<TTable>(this TTable table)
        where TTable : IInsertTable
        => new(table);
    #endregion
    #region ToInsert
    /// <summary>
    /// 插入Select
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static SelectInsert<TTable> ToInsert<TTable>(this TTable table, ISelect select)
        where TTable : IInsertTable
        => new(table, select);
    #endregion
    #region InsertTo
    /// <summary>
    /// 插入Select
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static SelectInsert<TTable> InsertTo<TTable>(this ISelect select, TTable table)
      where TTable : IInsertTable
      => new(table, select);
    /// <summary>
    /// 插入Select
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static SelectInsert<EmptyTable> InsertTo(this ISelect select, string tableName)
      => new(EmptyTable.Use(tableName), select);
    #endregion
}
