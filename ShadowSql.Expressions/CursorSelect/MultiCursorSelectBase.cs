using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Expressions.Select;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Expressions.CursorSelect;

/// <summary>
/// 多表游标筛选基类
/// </summary>
/// <param name="cursor">游标</param>
/// <param name="multiView">多(联)表</param>
public abstract class MultiCursorSelectBase(ICursor cursor, IMultiView multiView)
    : MultiSelectBase<ICursor>(cursor, multiView)
{
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
    #endregion
}
