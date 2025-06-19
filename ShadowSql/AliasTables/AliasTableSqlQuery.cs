using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.AliasTables;

/// <summary>
/// sql查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="aliasTable">别名表</param>
/// <param name="query">查询</param>
public class AliasTableSqlQuery<TTable>(IAliasTable<TTable> aliasTable, SqlQuery query)
    : DataFilterBase<IAliasTable<TTable>, SqlQuery>(aliasTable, query), IDataSqlQuery, IWhere
    where TTable : ITable
{
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
    /// 按逻辑查询
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public AliasTableSqlQuery<TTable> Where(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter.AddLogic(query(Prefix(select)));
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
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
