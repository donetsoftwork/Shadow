using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Filters;

/// <summary>
/// 数据筛选基类
/// </summary>
/// <typeparam name="TFilter"></typeparam>
/// <param name="source"></param>
/// <param name="filter"></param>
public abstract class DataFilterBase<TFilter>(ITableView source, TFilter filter)
    : FilterBase, ITableView, IDataFilter
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly ITableView _source = source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public ITableView Source
        => _source;
    /// <summary>
    /// 过滤条件
    /// </summary>
    internal TFilter _filter = filter;
    /// <summary>
    /// 过滤条件
    /// </summary>
    public TFilter Filter
        => _filter;
    #endregion
    #region IDataFilter
    ITableView IDataFilter.Source
        => _source;
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion
    #region ITableView
    /// <summary>
    /// 获取列
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IColumn> GetColumns()
        => _source.Columns;
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columName)
        => _source.GetColumn(columName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField Field(string fieldName)
        => _source.Field(fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写数据源(表)sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    /// <summary>
    /// 筛选条件可选
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _filter.TryWrite(engine, sql);
    #endregion
}
