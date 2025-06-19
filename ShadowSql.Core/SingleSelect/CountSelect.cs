using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 计数选择
/// </summary>
/// <param name="view"></param>
public class CountSelect(ITableView view) : ISingleSelect
{
    #region 配置
    private readonly ITableView _source = view;
    /// <summary>
    /// 数据源
    /// </summary>
    public ITableView Source 
        => _source;
    IFieldView ISingleSelect.SingleField
        => CountAliasFieldInfo.Use();
    #endregion
    #region ISelectFields
    IEnumerable<IFieldView> ISelectFields.Selected
        => [];
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => [];
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
    {
        engine.Count(sql);
        return true;
    }
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    #endregion
}
