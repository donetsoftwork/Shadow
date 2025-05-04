using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 游标单列选择
/// </summary>
/// <param name="cursor"></param>
/// <param name="singleField"></param>
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
    /// <summary>
    /// 拼写分页sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void Write(ISqlEngine engine, StringBuilder sql) 
        => engine.SelectCursor(sql, this, _cursor);
    #endregion
}
