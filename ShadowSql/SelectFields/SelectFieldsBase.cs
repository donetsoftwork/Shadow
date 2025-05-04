using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SelectFields;

/// <summary>
/// 筛选字段基类
/// </summary>
/// <typeparam name="TTarget"></typeparam>
/// <param name="target"></param>
public abstract class SelectFieldsBase<TTarget>(TTarget target)
    : SelectFieldsBase, ISelectFields
    where TTarget : ITableView
{
    #region 配置
    /// <summary>
    /// 筛选对象
    /// </summary>
    internal readonly TTarget _target = target;
    /// <summary>
    /// 筛选对象
    /// </summary>
    public TTarget Target
        => _target;
    #endregion
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    protected override IColumn? GetColumn(string columnName)
        => _target.GetColumn(columnName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField Field(string fieldName)
        => _target.Field(fieldName);
    #region ISelectFields
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
        => WriteSelectedCore(engine, sql, false);
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => ToColumnsCore();
    #endregion
}
