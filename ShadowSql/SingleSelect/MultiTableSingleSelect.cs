using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 多表视图筛选单列
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
public class MultiTableSingleSelect<TSource>(IMultiView multiTable, MultiTableFields fields)
    : SingleSelectBase<IMultiView, MultiTableFields>(multiTable, fields)
{
    /// <summary>
    /// 多表视图筛选单列
    /// </summary>
    /// <param name="multiTable"></param>
    public MultiTableSingleSelect(IMultiView multiTable)
        : this(multiTable, new MultiTableFields(multiTable))
    {
    }
}
/// <summary>
/// 多表视图范围(分页)及单列筛选
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class MultiTableCursorSingleSelect<TSource>(MultiTableCursor cursor, MultiTableFields fields)
    : SingleSelectBase<ICursor, MultiTableFields>(cursor, fields)
{
    /// <summary>
    /// 多表视图范围(分页)及单列筛选
    /// </summary>
    /// <param name="cursor"></param>
    public MultiTableCursorSingleSelect(MultiTableCursor cursor)
        : this(cursor, new MultiTableFields(cursor.Source))
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
