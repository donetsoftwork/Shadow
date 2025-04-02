using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Orders;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Fetches;

/// <summary>
/// 范围筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class FetchBase<TSource>(TSource source, int limit, int offset)
    : FetchBase(limit, offset), IFetch, ITableView
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
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columnName)
        => _source.GetColumn(columnName);
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
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
    IEnumerable<IColumn> ITableView.Columns
        => _source.Columns;
    IField ITableView.Field(string fieldName)
        => _source.Field(fieldName);
    IColumn? ITableView.GetColumn(string columName)
        => _source.GetColumn(columName);
    ICompareField ITableView.GetCompareField(string fieldName)
        => _source.GetCompareField(fieldName);
    #endregion
}
/// <summary>
/// 范围筛选基类
/// </summary>
/// <param name="limit"></param>
/// <param name="offset"></param>
public abstract class FetchBase(int limit, int offset)
    : ICursor
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
    protected readonly List<IOrderView> _fields = [];
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
    /// <param name="offset"></param>
    internal void SkipCore(int offset)
        => _offset += offset;
    /// <summary>
    /// 获取数量
    /// </summary>
    /// <param name="limit"></param>
    internal void TakeCore(int limit)
        => _limit = limit;
    #region IOrderField
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="field"></param>
    internal void AscCore(IOrderView field)
    {
        _fields.Add(field);
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="field"></param>
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
        _fields.Add(OrderRawFieldInfo.Use(orderBy));
    }
    #endregion
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public abstract IColumn? GetColumn(string columnName);
    #endregion
    /// <summary>
    /// 确认正序
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    private IOrderView CheckAsc(string fieldName)
    {
        if (GetColumn(fieldName) is IColumn column)
            return column;
        return OrderByField.Use(fieldName);
    }
    /// <summary>
    /// 确认倒叙
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    private IOrderView CheckDesc(string fieldName)
    {
        if (GetColumn(fieldName) is IColumn column)
            return column.Desc();
        return OrderByDescField.Use(fieldName);
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
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
    ICursor ICursor.Skip(int offset)
    {
        SkipCore(offset);
        return this;
    }
    #endregion
}

