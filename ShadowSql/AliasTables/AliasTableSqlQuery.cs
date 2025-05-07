using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;

namespace ShadowSql.AliasTables;

/// <summary>
/// sql查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="query"></param>
public class AliasTableSqlQuery<TTable>(TableAlias<TTable> table, SqlQuery query)
    : DataFilterBase<TableAlias<TTable>, SqlQuery>(table, query), IDataSqlQuery, IWhere
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
    #region 查询扩展
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
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
    /// <param name="select"></param>
    /// <returns></returns>
    protected IPrefixField Prefix(Func<TTable, IColumn> select)
        => _source.Prefix(select(_table));
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
