using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 获取表视图数据
/// </summary>
/// <param name="table"></param>
public class TableSelect(ITableView table) : SelectFieldsBase, ISelect
{
    #region 配置
    private readonly ITableView _source = table;
    /// <summary>
    /// 表视图
    /// </summary>
    public ITableView Source
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
         => engine.Select(sql, this);
    #endregion
}
