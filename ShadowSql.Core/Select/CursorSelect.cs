using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 范围(分页)及列筛选
/// </summary>
/// <param name="cursor"></param>
public sealed class CursorSelect(ICursor cursor) : SelectFieldsBase, ISelect
{
    #region 配置
    private readonly ICursor _source = cursor;
    /// <summary>
    /// 表视图
    /// </summary>
    public ICursor Source
        => _source;
    ITableView ISelect.Source
        => _source;
    #endregion
    #region ITableView
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    protected override IColumn? GetColumn(string columnName)
        => _source.GetColumn(columnName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField Field(string fieldName)
        => _source.Field(fieldName);
    #endregion
    #region ISelect
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => ToColumnsCore();
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
        => WriteSelectedCore(engine, sql, false);
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
         => engine.SelectCursor(sql, this, _source);
    #endregion
}
