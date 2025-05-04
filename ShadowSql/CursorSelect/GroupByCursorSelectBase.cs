using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.CursorSelect;

/// <summary>
/// 游标分组筛选基类
/// </summary>
/// <typeparam name="TGroupSource"></typeparam>
/// <param name="source"></param>
/// <param name="target"></param>
/// <param name="groupSource"></param>
public abstract class GroupCursorBySelectBase<TGroupSource>(ICursor source, IGroupByView target, TGroupSource groupSource)
    : GroupBySelectBase<ICursor, TGroupSource>(source, target, groupSource), ISelect
    where TGroupSource : ITableView
{
    #region ISqlEntity
    /// <summary>
    /// 拼写分页sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
    #endregion
}