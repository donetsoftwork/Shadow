using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Expressions.Cursors;

/// <summary>
/// 范围筛选游标基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="limit">筛选数量</param>
/// <param name="offset">跳过数量</param>
public class CursorBase<TSource>(TSource source, int limit, int offset)
    : CursorBase(limit, offset), ICursor
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 数据源
    /// </summary>
    protected readonly TSource _source = source;
    /// <summary>
    /// 数据源
    /// </summary>
    public TSource Source
        => _source;
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        WriteSource(engine, sql);
        WriteOrderBy(engine, sql);
    }
    /// <inheritdoc/>
    protected virtual void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
    #region TableViewBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _source.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _source.GetField(fieldName);
    /// <inheritdoc/>
    protected override ICompareField GetCompareField(string fieldName)
        => _source.GetCompareField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
         => _source.NewField(fieldName);
    #endregion
}

