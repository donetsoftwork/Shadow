using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class GroupByTableSelect<TTable>(IGroupByView groupBy, GroupByTableFields<TTable> fields)
    : SelectBase<IGroupByView, GroupByTableFields<TTable>>(groupBy, fields)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    public GroupByTableSelect(GroupByTableSqlQuery<TTable> groupBy)
        : this(groupBy, new GroupByTableFields<TTable>(groupBy))
    {
    }
}

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public class GroupByTableFetchSelect<TTable>(IFetch fetch, GroupByTableFields<TTable> fields)
    : SelectBase<IFetch, GroupByTableFields<TTable>>(fetch, fields)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="fetch"></param>
    public GroupByTableFetchSelect(GroupByTableFetch<TTable> fetch)
        : this(fetch, new GroupByTableFields<TTable>(fetch))
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