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
/// <param name="cursor">游标</param>
public sealed class CursorSelect(ICursor cursor) : SelectFieldsBase, ISelect
{
    #region 配置
    private readonly ICursor _source = cursor;
    /// <summary>
    /// 表视图
    /// </summary>
    public ICursor Source
        => _source;
    /// <inheritdoc/>
    ITableView ISelect.Source
        => _source;
    #endregion
    #region TableViewBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _source.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _source.GetField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
    #endregion
}
