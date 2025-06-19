using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Select;
using System.Collections.Generic;
using System;
using System.Text;
using ShadowSql.Identifiers;
using ShadowSql.Aggregates;

namespace ShadowSql.CursorSelect;

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <param name="cursor">游标</param>
public sealed class GroupByMultiCursorSelect(GroupByMultiCursor cursor)
    : GroupByMultiSelectBase<ICursor>(cursor, cursor.Source, cursor.MultiTable)
{
    #region ISqlEntity
    /// <summary>
    /// 拼写分页sql
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
    #endregion
    #region SelectAggregate
    #region TAliasTable
    /// <summary>
    /// 聚合筛选
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByMultiCursorSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IAggregateFieldAlias> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_groupSource.From<TAliasTable>(tableName)));
        return this;
    }
    /// <summary>
    /// 聚合筛选
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByMultiCursorSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IAggregateFieldAlias>> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_groupSource.From<TAliasTable>(tableName)));
        return this;
    }
    #endregion
    #region TTable
    /// <summary>
    /// 聚合筛选(先定位再聚合)
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByMultiCursorSelect SelectAggregate<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateFieldAlias> aggregate)
        where TTable : ITable
    {
        var member = _groupSource.Alias<TTable>(tableName);
        SelectCore(aggregate(member.Prefix(select(member.Target))));
        return this;
    }
    #endregion
    #endregion
}
