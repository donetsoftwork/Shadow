using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Text;

namespace ShadowSql.Fetches;

/// <summary>
/// 表范围筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="where"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public sealed class TableFetch<TTable>(TTable source, ISqlLogic where, int limit, int offset)
    : FetchBase<TTable>(source, offset, limit)
    where TTable : ITable
{
    #region 配置
    private readonly ISqlLogic _where = where;
    /// <summary>
    /// where查询条件
    /// </summary>
    public ISqlLogic Where
        => _where;
    #endregion 
    #region 排序
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public TableFetch<TTable> Asc(Func<TTable, IOrderView> select)
    {
        AscCore(select(_source));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public TableFetch<TTable> Desc(Func<TTable, IOrderAsc> select)
    {
        DescCore(select(_source));
        return this;
    }    
    #endregion
    /// <summary>
    /// 拼写数据源(表)sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteSource(engine, sql);
        WriteFilter(engine, sql);
    }
    /// <summary>
    /// 筛选条件可选
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    private bool WriteFilter(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        engine.WherePrefix(sql);
        if (!_where.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
        return true;
    }
}
