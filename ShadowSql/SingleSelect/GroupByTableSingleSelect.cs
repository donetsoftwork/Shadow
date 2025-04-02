using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// GroupBy后再筛选单列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class GroupByTableSingleSelect<TTable>(IGroupByView groupBy, GroupByTableFields<TTable> fields)
    : SingleSelectBase<IGroupByView, GroupByTableFields<TTable>>(groupBy, fields)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <param name="groupBy"></param>
    public GroupByTableSingleSelect(GroupByTableSqlQuery<TTable> groupBy)
        : this(groupBy, new GroupByTableFields<TTable>(groupBy))
    {
    }
}
/// <summary>
/// GroupBy后再范围(分页)及单列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class GroupByTableFetchSingleSelect<TTable>(ICursor cursor, GroupByTableFields<TTable> fields)
    : SingleSelectBase<ICursor, GroupByTableFields<TTable>>(cursor, fields)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <param name="cursor"></param>
    public GroupByTableFetchSingleSelect(GroupByTableCursor<TTable> cursor)
        : this(cursor, new GroupByTableFields<TTable>(cursor))
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