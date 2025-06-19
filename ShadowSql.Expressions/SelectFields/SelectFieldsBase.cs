using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Expressions.SelectFields;

/// <summary>
/// 筛选字段基类
/// </summary>
/// <typeparam name="TTarget"></typeparam>
/// <param name="view"></param>
public abstract class SelectFieldsBase<TTarget>(ITableView view)
    : SelectFieldsBase, ISelectFields
{
    #region 配置
    /// <summary>
    /// 筛选对象
    /// </summary>
    internal readonly ITableView _target = view;
    /// <summary>
    /// 筛选对象
    /// </summary>
    public ITableView Target
        => _target;
    #endregion
    #region GetFieldBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _target.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _target.GetField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => _target.NewField(fieldName);
    #endregion
    #region ISelectFields
    /// <inheritdoc/>
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
        => WriteSelectedCore(engine, sql, false);
    /// <inheritdoc/>
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => ToColumnsCore();
    #endregion
}
