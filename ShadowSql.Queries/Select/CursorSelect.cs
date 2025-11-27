using Hand.Models;
using Hand.Structural;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 范围(分页)及列筛选
/// </summary>
/// <param name="original">游标</param>
public sealed class CursorSelect(ICursor original)
    : SelectFieldsBase
    , ISelect
    , IWrapper<ICursor>
{
    #region 配置
    private readonly ICursor _original = original;
    /// <summary>
    /// 筛选视图
    /// </summary>
    public ICursor Original
        => _original;
    /// <inheritdoc/>
    ITableView ISelect.Original
        => _original;
    #endregion
    #region TableViewBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _original.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _original.GetField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => _original.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _original);
    #endregion
}
