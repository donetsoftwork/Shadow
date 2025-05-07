using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System;
using System.Collections.Generic;
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
    #region TableViewBase
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IField> GetFields()
        => _source.Fields;
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField? GetField(string fieldName)
        => _source.GetField(fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    #endregion
}
