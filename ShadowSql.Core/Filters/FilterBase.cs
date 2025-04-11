using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Filters;

/// <summary>
/// 过滤基类
/// </summary>
public abstract class FilterBase : ITableView
{    
    #region ITableView
    /// <summary>
    /// 获取所有列
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<IColumn> GetColumns();
    /// <summary>
    /// 构造字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public virtual IField Field(string fieldName)
        => FieldInfo.Use(fieldName);
    /// <summary>
    /// 获取单个列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IColumn? GetColumn(string columName);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    internal virtual ICompareField GetCompareField(string fieldName)
    {
        if (GetColumn(fieldName) is IColumn column)
            return column;
        return Field(fieldName);
    }
    /// <summary>
    /// 所有列
    /// </summary>
    IEnumerable<IColumn> ITableView.Columns
        => GetColumns();
    ICompareField ITableView.GetCompareField(string fieldName)
        => GetCompareField(fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void WriteSource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 拼写过滤条件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract bool WriteFilter(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 筛选前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual void FilterPrefix(ISqlEngine engine, StringBuilder sql)
        => engine.WherePrefix(sql);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    internal void Write(ISqlEngine engine, StringBuilder sql)
    {
        WriteSource(engine, sql);
        var point = sql.Length;
        FilterPrefix(engine, sql);
        if (!WriteFilter(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
    }
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => Write(engine, sql);
    #endregion
}
