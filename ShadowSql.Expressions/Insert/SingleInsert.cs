using ShadowSql.Engines;
using ShadowSql.Expressions.Visit;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShadowSql.Expressions.Insert;

/// <summary>
/// 插入单条
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="table">表</param>
/// <param name="items"></param>
public class SingleInsert<TEntity>(ITable table, List<IInsertValue> items)
    : SingleInsertBase(items), ISingleInsert
{
    /// <summary>
    /// 插入单条
    /// </summary>
    /// <param name="table">表</param>
    public SingleInsert(ITable table)
        : this(table, [])
    {
    }
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected readonly ITable _table = table;
    /// <summary>
    /// 源表
    /// </summary>
    public ITable Table
        => _table;
    IInsertTable IInsert.Table
        => _table;
    #endregion
    /// <summary>
    /// 增加插入值
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public SingleInsert<TEntity> Insert(Expression<Func<TEntity>> select)
    {
        var visitor = new SingleInsertVisitor(_table, _items);
        visitor.Visit(select.Body);
        return this;
    }
    /// <summary>
    /// 增加插入值
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public SingleInsert<TEntity> Insert<TParameter>(Expression<Func<TParameter, TEntity>> select)
    {
        var visitor = new SingleInsertVisitor(_table, _items);
        visitor.Visit(select.Body);
        return this;
    }
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteInsert(_table, engine, sql);
}
