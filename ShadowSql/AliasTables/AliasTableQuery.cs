using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.AliasTables;

/// <summary>
/// 逻辑查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="aliasTable">别名表</param>
/// <param name="filter">过滤条件</param>
public class AliasTableQuery<TTable>(IAliasTable<TTable> aliasTable, Logic filter)
    : DataFilterBase<IAliasTable<TTable>, Logic>(aliasTable, filter), IDataQuery
    where TTable : ITable
{
    /// <summary>
    /// 逻辑查询别名表
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    public AliasTableQuery(IAliasTable<TTable> aliasTable)
        : this(aliasTable, new AndLogic())
    {
    }
    #region 配置
    private readonly TTable _table = aliasTable.Target;
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
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public AliasTableQuery<TTable> And(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter = _filter.And(query(Prefix(select)));
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
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
    /// <param name="select">筛选</param>
    /// <returns></returns>
    protected IPrefixField Prefix(Func<TTable, IColumn> select)
        => _source.Prefix(select(_table));
    #region IDataQuery
    /// <inheritdoc/>
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
