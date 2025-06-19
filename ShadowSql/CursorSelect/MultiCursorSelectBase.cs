using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.CursorSelect;

/// <summary>
/// 多表游标筛选基类
/// </summary>
/// <param name="cursor">游标</param>
/// <param name="multiView">多(联)表</param>
public abstract class MultiCursorSelectBase(ICursor cursor, IMultiView multiView)
    : MultiSelectBase<ICursor>(cursor, multiView)
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
