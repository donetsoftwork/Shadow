using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Filters;

/// <summary>
/// 过滤基类
/// </summary>
public abstract class FilterBase : ITableView
{
    /// <summary>
    /// 添加子逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal abstract void AddLogic(AtomicLogic condition);
    /// <summary>
    /// 获取所有列
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<IColumn> GetColumns();
    #region ITableView
    /// <summary>
    /// 构造字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField Field(string fieldName);
    /// <summary>
    /// 获取单个列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IColumn? GetColumn(string columName);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public abstract void Write(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 所有列
    /// </summary>
    IEnumerable<IColumn> ITableView.Columns
        => GetColumns();
    #endregion
}
