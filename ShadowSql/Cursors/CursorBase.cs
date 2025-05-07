using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Cursors;

/// <summary>
/// 范围筛选游标基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
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
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        WriteSource(engine, sql);
        WriteOrderBy(engine, sql);
    }
    /// <summary>
    /// 拼写数据源(表)sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected virtual void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
    #region ITableView
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
    IEnumerable<IColumn> ITableView.Columns
        => _source.Columns;
    IColumn? ITableView.GetColumn(string columName)
        => GetColumn(columName);
    IField ITableView.Field(string fieldName)
        => Field(fieldName);
    ICompareField ITableView.GetCompareField(string fieldName)
        => _source.GetCompareField(fieldName);
    #endregion
    #region ICursor
    ICursor ICursor.Skip(int offset)
    {
        SkipCore(offset);
        return this;
    }
    #endregion
}

