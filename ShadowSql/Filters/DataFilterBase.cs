using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Filters;

/// <summary>
/// 数据筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TFilter"></typeparam>
/// <param name="source"></param>
/// <param name="filter"></param>
public abstract class DataFilterBase<TSource, TFilter>(TSource source, TFilter filter)
    : FilterBase, ITableView, IDataFilter
    where TSource : ITableView
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly TSource _source = source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public TSource Source
        => _source;
    /// <summary>
    /// 过滤条件
    /// </summary>
    internal TFilter _filter = filter;
    #endregion
    #region IDataFilter
    ITableView IDataFilter.Source
        => _source;
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion   
    #region FilterBase
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
