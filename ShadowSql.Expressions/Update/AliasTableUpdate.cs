using ShadowSql.Engines;
using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Visit;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Linq.Expressions;
using System.Text;

namespace ShadowSql.Expressions.Update;

/// <summary>
/// 修改别名表
/// </summary>
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class AliasTableUpdate<TEntity>(AliasUpdateTable table, ISqlLogic filter)
    : ExpressionUpdateBase<AliasUpdateTable>(table)
{
    /// <summary>
    /// 修改别名表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    public AliasTableUpdate(IAliasTable<ITable> table, ISqlLogic filter)
        : this(new AliasUpdateTable(table), filter)
    {
    }
    #region 配置
    /// <summary>
    /// 过滤条件
    /// </summary>
    protected readonly ISqlLogic _filter = filter;
    /// <summary>
    /// 过滤条件
    /// </summary>
    public ISqlLogic Filter
        => _filter;
    #endregion
    /// <summary>
    /// 更新属性
    /// </summary>
    /// <param name="operation">更新操作</param>
    /// <returns></returns>
    public AliasTableUpdate<TEntity> Set(Expression<Func<TEntity, TEntity>> operation)
    {
        var visitor = new UpdateVisitor(new TableVisitor(operation.Parameters[0], _source.Source), _assignInfos);
        visitor.Visit(operation.Body);
        return this;
    }
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteUpdate(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteUpdate(engine, sql);
        sql.Append(_source.Alias);
    }
    /// <inheritdoc/>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(" FROM ");
        base.WriteSource(engine, sql);
        var point = sql.Length;
        engine.WherePrefix(sql);
        if (!_filter.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
    }    
    #endregion
}
