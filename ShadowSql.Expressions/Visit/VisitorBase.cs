using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 解析表达式基类
/// </summary>
public abstract class VisitorBase(IFieldProvider source)
    : ExpressionVisitor
{
    #region 配置
    /// <summary>
    /// 字段来源
    /// </summary>
    protected readonly IFieldProvider _source = source;
    /// <summary>
    /// 字段来源
    /// </summary>
    public IFieldProvider Source
        => _source;
    #endregion
    #region VisitNew
    /// <summary>
    /// 解析NewExpression
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override Expression VisitNew(NewExpression node)
    {
        var members = node.Members;
        var arguments = node.Arguments;
        if (members is null || arguments is null)
            return node;
        for (int i = 0; i < arguments.Count; i++)
            CheckAssignment(arguments[i], members[i]);
        return node;
    }
    #endregion
    #region VisitMember
    /// <summary>
    /// 解析MemberExpression
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    protected override Expression VisitMember(MemberExpression member)
    {
        CheckMember(member);
        return member;
    }
    /// <summary>
    /// 处理属性
    /// </summary>
    /// <param name="member"></param>
    protected virtual void CheckMember(MemberExpression member)
    {
    }
    #endregion
    //#region VisitMemberInit
    //protected override Expression VisitMemberInit(MemberInitExpression node)
    //{
    //    return base.VisitMemberInit(node);
    //}
    //protected virtual void CheckMemberInitBinding(MemberInitExpression node)
    //{
    //}
    //#endregion
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
    /// <param name="expression"></param>
    /// <param name="info"></param>
    protected virtual void CheckAssignment(Expression expression,  MemberInfo info)
    {            
    }
    #endregion
    /// <summary>
    /// 解析BinaryExpression
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override Expression VisitBinary(BinaryExpression node)
    {
        CheckBinary(node.NodeType, node.Left, node.Right);
        return node;
    }
    /// <summary>
    /// 处理二元表达式
    /// </summary>
    /// <param name="op">操作类型</param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    protected virtual void CheckBinary(ExpressionType op, Expression left, Expression right)
    {
    }
    /// <summary>
    /// 解析UnaryExpression
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override Expression VisitUnary(UnaryExpression node)
    {
        CheckUnary(node.NodeType, node.Operand);
        return node;
    }
    /// <summary>
    /// 处理一元表达式
    /// </summary>
    /// <param name="op">操作类型</param>
    /// <param name="expression"></param>
    protected virtual void CheckUnary(ExpressionType op, Expression expression)
    {
    }
    /// <summary>
    /// 解析MethodCallExpression
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    protected override Expression VisitMethodCall(MethodCallExpression method)
    {
        CheckMethodCall(method);
        return method;
    }
    /// <summary>
    /// 处理方法调用
    /// </summary>
    /// <param name="method"></param>
    protected virtual void CheckMethodCall(MethodCallExpression method)
    {
    }
}
