using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Variants;
using System;

namespace ShadowSql.Queries;

/// <summary>
/// 查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="query"></param>
public class AliasTableQuery<TTable>(TableAlias<TTable> table, SqlQuery query)
    : DataQuery<TableAlias<TTable>>(table, query)
    where TTable : ITable
{
    #region 配置
    private readonly TTable _table = table.Target;
    /// <summary>
    /// 原始表
    /// </summary>
    public TTable Table
        => _table;
    #endregion
    #region 基础查询
    ///// <summary>
    ///// 按原始sql查询
    ///// </summary>
    ///// <param name="conditions"></param>
    ///// <returns></returns>
    //public AliasTableQuery<TTable> Where(params IEnumerable<string> conditions)
    //{
    //    AddConditions(conditions);
    //    return this;
    //}
    ///// <summary>
    ///// 按逻辑查询
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public AliasTableQuery<TTable> Where(IAtomicLogic logic)
    //{
    //    AddLogic(logic);
    //    return this;
    //}
    ///// <summary>
    ///// Where
    ///// </summary>
    ///// <param name="query"></param>
    ///// <returns></returns>
    //public AliasTableQuery<TTable> Where(Func<SqlQuery, SqlQuery> query)
    //{
    //    ApplyQuery(query);
    //    return this;
    //}
    /// <summary>
    /// Where
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TTable> Where(Func<IAliasTable, SqlQuery, SqlQuery> query)
    {
        ApplyQuery(query);
        return this;
    }
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    ///// <returns></returns>
    //public AliasTableQuery<TTable> ToAnd()
    //{
    //    ToAndCore();
    //    return this;
    //}
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    ///// <returns></returns>
    //public AliasTableQuery<TTable> ToOr()
    //{
    //    ToOrCore();
    //    return this;
    //}
    #endregion
    #region 查询扩展
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TTable> Where(Func<IAliasTable, AtomicLogic> query)
    {
        AddLogic(query(_source));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="column"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TTable> Where(Func<TTable, IColumn> column, Func<IColumn, AtomicLogic> query)
    {
        //增加前缀
        var prefixColumn = _source.GetPrefixColumn(column(_table));
        if (prefixColumn is not null)
            AddLogic(query(prefixColumn));
        return this;
    }
    #endregion
}
