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
/// <param name="cursor">游标</param>
/// <param name="groupBy">分组查询</param>
/// <param name="view"></param>
public abstract class GroupCursorBySelectBase<TGroupSource>(ICursor cursor, IGroupByView groupBy, TGroupSource view)
    : GroupBySelectBase<ICursor, TGroupSource>(cursor, groupBy, view), ISelect
    where TGroupSource : ITableView
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
}