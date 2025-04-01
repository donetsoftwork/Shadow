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
    : DataSqlQuery<TableAlias<TTable>>(table, query), IWhere
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
    /// <param name="column"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableSqlQuery<TTable> Where(Func<TTable, IColumn> column, Func<IColumn, AtomicLogic> query)
    {
        //增加前缀
        var prefixColumn = _source.GetPrefixColumn(column(_table));
        if (prefixColumn is not null)
            AddLogic(query(prefixColumn));
        return this;
    }
    #endregion
}
