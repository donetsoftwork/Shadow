using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;

namespace ShadowSql.AliasTables;

/// <summary>
/// 逻辑查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="filter"></param>
public class AliasTableQuery<TTable>(TableAlias<TTable> table, Logic filter)
    : DataFilterBase<TableAlias<TTable>, Logic>(table, filter), IDataQuery
    where TTable : ITable
{
    /// <summary>
    /// 逻辑查询别名表
    /// </summary>
    /// <param name="table"></param>
    public AliasTableQuery(TableAlias<TTable> table)
        : this(table, new AndLogic())
    {
    }
    #region 配置
    private readonly TTable _table = table.Target;
    /// <summary>
    /// 原始表
    /// </summary>
    public TTable Table
        => _table;
    #endregion
    #region 查询扩展
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TTable> And(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter = _filter.And(query(Prefix(select)));
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TTable> Or(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter = _filter.Or(query(Prefix(select)));
        return this;
    }
    #endregion
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    protected IPrefixField Prefix(Func<TTable, IColumn> select)
        => _source.Prefix(select(_table));
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
