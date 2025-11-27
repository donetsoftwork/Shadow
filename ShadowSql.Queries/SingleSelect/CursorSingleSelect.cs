using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 游标单列选择
/// </summary>
/// <param name="original">游标</param>
/// <param name="singleField">单列</param>
public class CursorSingleSelect(ICursor original, IFieldView singleField)
    : TableSingleSelect(original, singleField)
{
    #region 配置
    private readonly ICursor _original = original;
    /// <summary>
    /// 游标
    /// </summary>
    public new ICursor Original
        => _original;
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void Write(ISqlEngine engine, StringBuilder sql) 
        => engine.SelectCursor(sql, this, _original);
    #endregion
}
