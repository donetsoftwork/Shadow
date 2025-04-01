using ShadowSql.Engines;
using ShadowSql.Logics;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// 数据筛选基类
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <typeparam name="TFilter"></typeparam>
/// <param name="source"></param>
/// <param name="filter"></param>
public abstract class DataFilterBase<TTable, TFilter>(TTable source, TFilter filter)
    : ITableView
    where TTable : ITableView
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly TTable _source = source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public TTable Source
        => _source;
    /// <summary>
    /// 过滤条件
    /// </summary>
    protected TFilter _filter = filter;
    /// <summary>
    /// 过滤条件
    /// </summary>
    public TFilter Filter
        => _filter;
    #endregion
    #region ITableView
    IEnumerable<IColumn> ITableView.Columns
        => _source.Columns;
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public virtual IColumn? GetColumn(string columName)
        => _source.GetColumn(columName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public virtual IField Field(string fieldName)
        => _source.Field(fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        WriteSource(engine, sql);
        WriteFilter(engine, sql);
    }
    /// <summary>
    /// 筛选条件可选
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual bool WriteFilter(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        FilterPrefix(engine, sql);
        if (!_filter.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
        return true;
    }
    /// <summary>
    /// 拼写数据源(表)sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected virtual void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        _source.Write(engine, sql);
    }
    /// <summary>
    /// 筛选前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual void FilterPrefix(ISqlEngine engine, StringBuilder sql)
    {
        engine.WherePrefix(sql);
    }
    #endregion
}
