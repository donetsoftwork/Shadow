using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.SqlVales;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 单条插入表达式解析
/// </summary>
/// <param name="table">表</param>
/// <param name="items"></param>
public class SingleInsertVisitor(ITable table, List<IInsertValue> items)
    : ExpressionVisitor
{
    /// <summary>
    /// 单条插入表达式解析
    /// </summary>
    /// <param name="table">表</param>
    public SingleInsertVisitor(ITable table)
        : this(table, [])
    {
    }
    #region 配置
    private readonly ITable _table = table;
    private readonly List<IInsertValue> _items = items;
    /// <summary>
    /// 表
    /// </summary>
    public ITable Table 
        => _table;
    /// <summary>
    /// 
    /// </summary>
    public List<IInsertValue> Items 
        => _items;
    #endregion
    private IColumn GetColumn(string columnName)
        => _table.GetInsertColumn(columnName) ?? Column.Use(columnName);

    #region VisitMemberAssignment
    /// <summary>
    /// 解析MemberAssignment
    /// </summary>
    /// <param name="assignment"></param>
    /// <returns></returns>
    protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
    {
        CheckAssignment(assignment.Expression, assignment.Member);
        return assignment;
    }
    /// <summary>
    /// 处理赋值
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <param name="info"></param>
    protected virtual void CheckAssignment(Expression expression, MemberInfo info)
    {
        if (GetValue(expression) is ISqlValue value)
            _items.Add(new InsertValue(GetColumn(info.Name), value));
    }
    #endregion
    private static ISqlValue? GetValue(Expression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.MemberAccess:
                if (expression is MemberExpression member)
                {
                    if (member.Expression is ConstantExpression)
                        return LogicVisitor.GetSqlValue(Expression.Lambda(member).Compile().DynamicInvoke());
                    return Parameter.Use(member.Member.Name);
                }
                break;
            case ExpressionType.Constant:
                if (expression is ConstantExpression constant)
                    return LogicVisitor.GetSqlValue(constant.Value);
                break;
            case ExpressionType.Convert:
                if (expression is UnaryExpression unary)
                    return GetValue(unary.Operand);
                break;
            default:
                break;
        }
        return null;
    }
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static SingleInsertVisitor Insert<TEntity>(ITable table, Expression<Func<TEntity>> select)
    {
        SingleInsertVisitor visitor = new(table);
        visitor.Visit(select.Body);
        return visitor;
    }
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static SingleInsertVisitor Insert<TParameter, TEntity>(ITable table, Expression<Func<TParameter, TEntity>> select)
    {
        SingleInsertVisitor visitor = new(table);
        visitor.Visit(select.Body);
        return visitor;
    }
}
