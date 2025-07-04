using ShadowSql.Engines;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Linq.Expressions;
using System.Text;

namespace ShadowSql.Expressions.Cursors;

/// <summary>
/// 表范围筛选游标
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public sealed class TableCursor<TEntity> : CursorBase<ITableView>
{
    internal TableCursor(ISqlLogic where, ITableView source, int limit, int offset)
        : base(source, limit, offset)
    {
        _where = where;
    }
    /// <summary>
    /// 表范围筛选游标
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public TableCursor(ITable table, ISqlLogic where, int limit, int offset)
        : this(where, table, limit, offset) { }
    /// <summary>
    /// 别名表范围筛选游标
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public TableCursor(IAliasTable aliasTable, ISqlLogic where, int limit, int offset)
        : this(where, aliasTable, limit, offset) { }
    #region 配置
    private readonly ISqlLogic _where;
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
    public TableCursor<TEntity> Asc<TProperty>(Expression<Func<TEntity, TProperty>> select)
    {
        var fields = TableVisitor.GetFieldsByExpression(select, _source);
        foreach (var field in fields)
            AscCore(field);
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public TableCursor<TEntity> Desc<TProperty>(Expression<Func<TEntity, TProperty>> select)
    {
        var fields = TableVisitor.GetFieldsByExpression(select, _source);
        foreach (var field in fields)
            DescCore(field);
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
