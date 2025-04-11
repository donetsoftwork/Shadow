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
/// <param name="source"></param>
public class CountSelect(ITableView source) : ISingleSelect
{
    #region 配置
    private readonly ITableView _source = source;
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
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    #endregion
}
