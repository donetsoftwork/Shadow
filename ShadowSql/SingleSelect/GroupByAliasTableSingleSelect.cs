﻿using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// GroupBy别名表后再筛选单列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class GroupByAliasTableSingleSelect<TTable>(IGroupByView groupBy, GroupByAliasTableFields<TTable> fields)
    : SingleSelectBase<IGroupByView, GroupByAliasTableFields<TTable>>(groupBy, fields)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再筛选单列
    /// </summary>
    /// <param name="groupBy"></param>
    public GroupByAliasTableSingleSelect(GroupByAliasTableSqlQuery<TTable> groupBy)
        : this(groupBy, new GroupByAliasTableFields<TTable>(groupBy))
    {
    }
}

/// <summary>
/// GroupBy别名表后再范围(分页)及列筛选单列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class GroupByAliasTableCursorSingleSelect<TTable>(ICursor cursor, GroupByAliasTableFields<TTable> fields)
    : SingleSelectBase<ICursor, GroupByAliasTableFields<TTable>>(cursor, fields)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选单列
    /// </summary>
    /// <param name="cursor"></param>
    public GroupByAliasTableCursorSingleSelect(GroupByAliasTableCursor<TTable> cursor)
        : this(cursor, new GroupByAliasTableFields<TTable>(cursor))
    {
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
}
