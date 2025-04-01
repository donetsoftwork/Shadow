using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组基类
/// </summary>
/// <typeparam name="TFilter"></typeparam>
/// <param name="fields"></param>
/// <param name="having"></param>
public abstract class GroupByBase<TFilter>(IFieldView[] fields, TFilter having)
    : GroupByBase(fields), IDataFilter
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 过滤条件
    /// </summary>
    protected TFilter _having = having;
    /// <summary>
    /// 过滤条件
    /// </summary>
    public TFilter Filter
        => _having;
    #endregion
    #region ApplyHaving
    /// <summary>
    /// 应用过滤
    /// </summary>
    /// <param name="having"></param>
    internal void ApplyHaving(Func<TFilter, TFilter> having)
        => _having = having(_having);
    #endregion
    #region IDataFilter
    ITableView IDataFilter.Source
        => GetFilterSource();
    ISqlLogic IDataFilter.Filter
        => _having;
    void IDataFilter.AddLogic(AtomicLogic condition)
        => AddLogic(condition);
    ICompareField IDataFilter.GetCompareField(string fieldName)
        => GetCompareField(fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 筛选条件可选
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _having.TryWrite(engine, sql);
    #endregion
}
/// <summary>
/// 分组基类
/// </summary>
public abstract class GroupByBase : FilterBase, IGroupByView
{
    /// <summary>
    /// 分组基类
    /// </summary>
    /// <param name="fields"></param>
    public GroupByBase(IFieldView[] fields)
    {
        _fields = fields;
        _columns = new(() => [.. _fields.Select(field => field.ToColumn())]);
    }
    #region 配置
    private readonly IFieldView[] _fields;
    /// <summary>
    /// 分组字段
    /// </summary>
    public IFieldView[] Fields
        => _fields;
    private readonly Lazy<IColumn[]> _columns;
    /// <summary>
    /// 分组列
    /// </summary>
    public IColumn[] Columns
        => _columns.Value;
    #endregion
    internal override ITableView GetFilterSource()
        => this;
    #region ISqlEntity
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        WriteGroupBySource(engine, sql);
        WriteGroupBy(engine, sql);
    }
    /// <summary>
    /// 拼写分组条件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected void WriteGroupBy(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        //必选的GROUP BY
        engine.GroupByPrefix(sql);
        var next = false;
        foreach (var field in _fields)
        {
            var point2 = sql.Length;
            if (next)
                sql.Append(',');
            field.Write(engine, sql);
            next = true;

        }
        if (!next)
        {
            //回滚
            sql.Length = point;
            throw new InvalidOperationException("分组字段不能为空");
        }
    }
    /// <summary>
    /// 分组数据源拼写
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected abstract void WriteGroupBySource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 筛选前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void FilterPrefix(ISqlEngine engine, StringBuilder sql)
        => engine.HavingPrefix(sql);
    #endregion
    /// <summary>
    /// 获取列
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IColumn> GetColumns()
        => _columns.Value;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columName)
        => _columns.Value.FirstOrDefault(c => c.IsMatch(columName));
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    internal override ICompareField GetCompareField(string fieldName)
    {
        if (_fields.FirstOrDefault(field => field.IsMatch(fieldName)) is ICompareField compareField)
            return compareField;
        return Field(fieldName);
    }
}