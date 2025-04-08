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
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class GroupByMultiSingleSelect(IGroupByView groupBy, GroupByMultiFields fields)
    : SingleSelectBase<IGroupByView, GroupByMultiFields>(groupBy, fields)
{
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <param name="groupBy"></param>
    public GroupByMultiSingleSelect(GroupByMultiSqlQuery groupBy)
        : this(groupBy, new GroupByMultiFields(groupBy))
    {
    }
}

/// <summary>
/// GroupBy后再范围(分页)及单列筛选
/// </summary>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class GroupByMultiCursorSingleSelect(ICursor cursor, GroupByMultiFields fields)
    : SingleSelectBase<ICursor, GroupByMultiFields>(cursor, fields)
{
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <param name="cursor"></param>
    public GroupByMultiCursorSingleSelect(GroupByMultiCursor cursor)
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