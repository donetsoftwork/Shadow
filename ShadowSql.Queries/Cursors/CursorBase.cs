using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Orders;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Cursors;

/// <summary>
/// 范围筛选基类
/// </summary>
/// <param name="limit">筛选数量</param>
/// <param name="offset">跳过数量</param>
public abstract class CursorBase(int limit, int offset) : TableViewBase, ICursor
{
    #region 配置
    /// <summary>
    /// 获取数量
    /// </summary>
    protected int _limit = limit;
    /// <summary>
    /// 跳过数量
    /// </summary>
    protected int _offset = offset;
    /// <summary>
    /// 排序字段
    /// </summary>
    internal readonly List<IOrderView> _fields = [];
    /// <summary>
    /// 获取数量
    /// </summary>
    public int Limit
        => _limit;
    /// <summary>
    /// 跳过数量
    /// </summary>
    public int Offset
        => _offset;
    #endregion
    #region 功能
    /// <summary>
    /// 跳过数量
    /// </summary>
    /// <param name="offset">跳过数量</param>
    internal void SkipCore(int offset)
        => _offset += offset;
    /// <summary>
    /// 获取数量
    /// </summary>
    /// <param name="limit">筛选数量</param>
    internal void TakeCore(int limit)
        => _limit = limit;
    #region IOrderField
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="field">字段</param>
    internal void AscCore(IOrderView field)
    {
        _fields.Add(field);
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="field">字段</param>
    internal void DescCore(IOrderAsc field)
    {
        _fields.Add(field.Desc());
    }
    #endregion
    #region fieldNames
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="fieldNames"></param>
    /// <returns></returns>
    internal void AscCore(params IEnumerable<string> fieldNames)
    {
        foreach (var field in fieldNames)
        {
            _fields.Add(CheckAsc(field));
        }
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="fieldNames"></param>
    /// <returns></returns>
    internal void DescCore(params IEnumerable<string> fieldNames)
    {
        foreach (var field in fieldNames)
        {
            _fields.Add(CheckDesc(field));
        }
    }
    #endregion
    #region raw
    /// <summary>
    /// 添加排序
    /// </summary>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    internal void OrderByCore(string orderBy)
    {
        _fields.Add(RawOrderByInfo.Use(orderBy));
    }
    #endregion
    #endregion
    /// <summary>
    /// 确认正序
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    protected IOrderView CheckAsc(string fieldName)
    {
        if (GetField(fieldName) is IField field)
            return field;
        return NewField(fieldName);
    }
    /// <summary>
    /// 确认倒序
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    protected IOrderView CheckDesc(string fieldName)
    {
        if (GetField(fieldName) is IField field)
            return field.Desc();
        return OrderByDescField.Use(fieldName);
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <returns></returns>
    protected bool WriteOrderBy(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        bool appended = false;
        sql.Append(" ORDER BY ");
        foreach (var item in _fields)
        {
            if (appended)
                sql.Append(',');
            item.Write(engine, sql);
            appended = true;
        }
        if (appended)
            return true;
        //回滚
        sql.Length = point;
        return false;
    }
    #region ICursor
    ICursor ICursor.Take(int limit)
    {
        TakeCore(limit);
        return this;
    }
    ICursor ICursor.Skip(int offset)
    {
        SkipCore(offset);
        return this;
    }
    #endregion
}

