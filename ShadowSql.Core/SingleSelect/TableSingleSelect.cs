using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 单列选择
/// </summary>
/// <param name="source"></param>
/// <param name="singleField"></param>
public class TableSingleSelect(ITableView source, IFieldView singleField)
    : ISingleSelect
{
    #region 配置
    private readonly ITableView _source = source;
    /// <summary>
    /// 表视图
    /// </summary>
    public ITableView Source
        => _source;
    private readonly IFieldView _singleField = singleField;
    /// <summary>
    /// 
    /// </summary>
    public IFieldView SingleField
        => _singleField;
    #endregion
    #region ISelectFields
    IEnumerable<IFieldView> ISelectFields.Selected
    {
        get { yield return _singleField; }
    }
    IEnumerable<IColumn> ISelectFields.ToColumns()
    {
        yield return _singleField.ToColumn();
    }
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
    {
        _singleField.Write(engine, sql);
        return true;
    }
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual void Write(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);    
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => Write(engine, sql);
    #endregion
}
