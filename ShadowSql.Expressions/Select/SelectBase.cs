using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Expressions.Select;

/// <summary>
/// 筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
/// <param name="source"></param>
/// <param name="target"></param>
public abstract class SelectBase<TSource, TTarget>(TSource source, TTarget target)
    : SelectFieldsBase, ISelect
    where TSource : ITableView
    where TTarget : ITableView
{
    #region 配置
    /// <summary>
    /// 数据源筛选
    /// </summary>
    protected readonly TSource _source = source;
    /// <summary>
    /// 数据源筛选
    /// </summary>
    public TSource Source 
        => _source;
    /// <summary>
    /// 筛选对象
    /// </summary>
    internal readonly TTarget _target = target;
    /// <summary>
    /// 筛选对象
    /// </summary>
    public TTarget Target
        => _target;
    ITableView ISelect.Source
        => _source;
    #endregion
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select">筛选</param>
    internal void SelectCore(Func<TTarget, IFieldView> select)
        => SelectCore(select(_target));
    #region GetFieldBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _source.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _source.GetField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    #endregion
}
