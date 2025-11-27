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
/// <param name="original">原始表</param>
/// <param name="filter">过滤条件</param>
public abstract class DataFilterBase<TFilter>(ITableView original, TFilter filter)
    : FilterBase, ITableView, IDataFilter
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly ITableView _original = original;
    /// <summary>
    /// 数据源表
    /// </summary>
    public ITableView Original
        => _original;
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
    /// <inheritdoc/>
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion
    #region FilterBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _original.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _original.GetField(fieldName);
    /// <inheritdoc/>
    protected override ICompareField GetCompareField(string fieldName)
        => _original.GetCompareField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => _original.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _original.Write(engine, sql);
    /// <inheritdoc/>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _filter.TryWrite(engine, sql);
    #endregion
}
