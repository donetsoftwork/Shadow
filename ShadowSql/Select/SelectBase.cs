using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System;
using System.Text;

namespace ShadowSql.Select;

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
    /// <param name="select"></param>
    internal void SelectCore(Func<TTarget, IFieldView> select)
        => SelectCore(select(_target));
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
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteCore(engine, sql);
    #endregion
}
