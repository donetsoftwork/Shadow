using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Fragments;
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
    /// 过滤查询数据源
    /// </summary>
    /// <returns></returns>
    internal abstract ITableView GetFilterSource();
    /// <summary>
    /// 添加子逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal abstract void AddLogic(AtomicLogic condition);
    /// <summary>
    /// 切换为And
    /// </summary>
    internal abstract void ToAndCore();
    /// <summary>
    /// 切换为Or
    /// </summary>
    internal abstract void ToOrCore();
    #region Logic
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void AndCore(AtomicLogic condition) { }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void AndCore(AndLogic condition) { }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void AndCore(ComplexAndLogic condition) { }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void AndCore(OrLogic condition) { }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void AndCore(ComplexOrLogic condition) { }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void AndCore(Logic condition) { }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void OrCore(AtomicLogic condition) { }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void OrCore(AndLogic condition) { }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void OrCore(ComplexAndLogic condition) { }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void OrCore(OrLogic condition) { }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void OrCore(ComplexOrLogic condition) { }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal virtual void OrCore(Logic condition) { }
    #endregion
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
