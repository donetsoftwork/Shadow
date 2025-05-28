using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Expressions.Select;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Expressions.CursorSelect;

/// <summary>
/// 游标筛选基类
/// </summary>
/// <typeparam name="TTarget"></typeparam>
public abstract class CursorSelectBase<TTarget>(ICursor source, TTarget target)
    : SelectBase<ICursor, TTarget>(source, target), ISelect
    where TTarget : ITableView
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
