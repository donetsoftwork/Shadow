using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Text;

namespace ShadowSql.Cursors;

/// <summary>
/// 表范围筛选游标
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table">表</param>
/// <param name="where">查询条件</param>
/// <param name="limit">筛选数量</param>
/// <param name="offset">跳过数量</param>
public sealed class TableCursor<TTable>(TTable table, ISqlLogic where, int limit, int offset)
    : CursorBase<TTable>(table, limit, offset)
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
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public TableCursor<TTable> Asc(Func<TTable, IOrderView> select)
    {
        AscCore(select(_source));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public TableCursor<TTable> Desc(Func<TTable, IOrderAsc> select)
    {
        DescCore(select(_source));
        return this;
    }
    #endregion
    /// <inheritdoc/>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteSource(engine, sql);
        WriteFilter(engine, sql);
    }
    /// <summary>
    /// 筛选条件可选
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
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
