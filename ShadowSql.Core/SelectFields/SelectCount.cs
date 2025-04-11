using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SelectFields;

/// <summary>
/// 计数选择
/// </summary>
public sealed class SelectCount : ISelectFields
{
    private SelectCount()
    {
    }
    /// <summary>
    /// 单例
    /// </summary>
    public readonly static SelectCount Instance = new();

    #region ISelectFields
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => [];
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
    {
        engine.Count(sql);
        return true;
    }
    IEnumerable<IFieldView> ISelectFields.Selected
        => [];
    #endregion
}
