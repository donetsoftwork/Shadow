using ShadowSql.Engines;
using ShadowSql.Expressions.Visit;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Linq.Expressions;
using System.Text;

namespace ShadowSql.Expressions.Update;

/// <summary>
/// 更新表
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class TableUpdate<TEntity>(ITable table, ISqlLogic filter)
    : ExpressionUpdateBase<ITable>(table)
{
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
    public TableUpdate<TEntity> Set(Expression<Func<TEntity, TEntity>> operation)
    {
        var visitor = new UpdateVisitor(new TableVisitor(_source, operation.Parameters[0]), _assignInfos);
        visitor.Visit(operation.Body);
        return this;
    }
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        WriteUpdate(engine, sql);
        WriteSource(engine, sql);
        WriteSet(engine, sql);
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
