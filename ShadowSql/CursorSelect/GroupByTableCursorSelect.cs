using ShadowSql.Aggregates;
using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.CursorSelect;

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
public class GroupByTableCursorSelect<TTable>(GroupByTableCursor<TTable> cursor)
    : GroupCursorBySelectBase<TTable>(cursor, cursor.Source, cursor.Table)
    where TTable : ITable
{
    /// <summary>
    /// 聚合筛选
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByTableCursorSelect<TTable> SelectAggregate(Func<TTable, IAggregateFieldAlias> select)
    {
        SelectCore(select(_groupSource));
        return this;
    }
}
