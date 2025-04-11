using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SelectFields;

/// <summary>
/// 筛选字段基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="view"></param>
public abstract class SelectFieldsBase<TSource>(TSource view)
    : SelectFieldsBase, ISelectFields
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 表视图
    /// </summary>
    protected readonly TSource _source = view;
    /// <summary>
    /// 表视图
    /// </summary>
    public TSource View
        => _source;
    #endregion
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columnName)
        => _source.GetColumn(columnName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField Field(string fieldName)
        => _source.Field(fieldName);

    #region ISelectFields
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
        => WriteSelectedCore(engine, sql, false);
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => ToColumnsCore();
    #endregion
}
