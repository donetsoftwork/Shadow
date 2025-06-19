using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Expressions.Select;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Expressions.CursorSelect;

/// <summary>
/// 游标分组筛选基类
/// </summary>
/// <typeparam name="TGroupSource"></typeparam>
/// <param name="cursor">游标</param>
/// <param name="groupBy">分组查询</param>
/// <param name="groupSource"></param>
public abstract class GroupCursorBySelectBase<TGroupSource>(ICursor cursor, IGroupByView groupBy, TGroupSource groupSource)
    : GroupBySelectBase<ICursor, TGroupSource>(cursor, groupBy, groupSource), ISelect
    where TGroupSource : ITableView
{
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
    #endregion
}