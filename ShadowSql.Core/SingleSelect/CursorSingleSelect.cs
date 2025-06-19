using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 游标单列选择
/// </summary>
/// <param name="cursor">游标</param>
/// <param name="singleField">单列</param>
public class CursorSingleSelect(ICursor cursor, IFieldView singleField)
    : TableSingleSelect(cursor, singleField)
{
    #region 配置
    private readonly ICursor _cursor = cursor;
    /// <summary>
    /// 游标
    /// </summary>
    public ICursor Cursor
        => _cursor;
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void Write(ISqlEngine engine, StringBuilder sql) 
        => engine.SelectCursor(sql, this, _cursor);
    #endregion
}
