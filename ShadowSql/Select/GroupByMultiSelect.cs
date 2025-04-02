using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public sealed class GroupByMultiSelect(IGroupByView groupBy, GroupByMultiFields fields)
    : SelectBase<IGroupByView, GroupByMultiFields>(groupBy, fields)
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    public GroupByMultiSelect(GroupByMultiSqlQuery groupBy)
        : this(groupBy, new GroupByMultiFields(groupBy))
    {
    }
}

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public sealed class GroupByMultiFetchSelect(ICursor cursor, GroupByMultiFields fields)
    : SelectBase<ICursor, GroupByMultiFields>(cursor, fields)
{
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    public GroupByMultiFetchSelect(GroupByMultiCursor cursor)
        : this(cursor, new GroupByMultiFields(cursor))
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