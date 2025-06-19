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
/// <param name="view"></param>
/// <param name="singleField">单列</param>
public class TableSingleSelect(ITableView view, IFieldView singleField)
    : ISingleSelect
{
    #region 配置
    private readonly ITableView _source = view;
    /// <inheritdoc/>
    public ITableView Source
        => _source;
    private readonly IFieldView _singleField = singleField;
    /// <inheritdoc/>
    public IFieldView SingleField
        => _singleField;
    #endregion
    #region ISelectFields
    /// <inheritdoc/>
    IEnumerable<IFieldView> ISelectFields.Selected
    {
        get { yield return _singleField; }
    }
    /// <inheritdoc/>
    IEnumerable<IColumn> ISelectFields.ToColumns()
    {
        yield return _singleField.ToColumn();
    }
    /// <inheritdoc/>
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
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    protected virtual void Write(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => Write(engine, sql);
    #endregion
}
