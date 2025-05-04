using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.CursorSelect;

/// <summary>
/// 多表游标筛选基类
/// </summary>
/// <param name="source"></param>
/// <param name="target"></param>
public abstract class MultiCursorSelectBase(ICursor source, IMultiView target)
    : MultiSelectBase<ICursor>(source, target)
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
