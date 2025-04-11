using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TFields"></typeparam>
/// <param name="source"></param>
/// <param name="fields"></param>
public abstract class SelectBase<TSource, TFields>(TSource source, TFields fields)
    : ISelect
    where TSource : ITableView
    where TFields : ISelectFields
{
    //桥接模式:TSource、TFields和Select三者独立变化
    #region 配置
    /// <summary>
    /// 数据源筛选
    /// </summary>
    protected readonly TSource _source = source;
    /// <summary>
    /// 字段筛选
    /// </summary>
    protected readonly TFields _fields = fields;

    /// <summary>
    /// 数据源筛选
    /// </summary>
    public TSource Source 
        => _source;
    /// <summary>
    /// 字段筛选
    /// </summary>
    public TFields Fields
        => _fields;

    /// <summary>
    /// 确认字段
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<IFieldView> CheckFields()
        => _fields.Selected;
    #endregion

    #region ISqlFragment
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    /// <summary>
    /// 拼写字段
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public virtual bool WriteSelected(ISqlEngine engine, StringBuilder sql)
        => _fields.WriteSelected(engine, sql);

    IEnumerable<IColumn> ISelectFields.ToColumns()
        => _fields.ToColumns();
    #endregion

    #region ISelect
    ITableView ISelect.Source
        => _source;
    IEnumerable<IFieldView> ISelectFields.Selected
        => CheckFields();
    #endregion
}
