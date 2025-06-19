using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Expressions.Services;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SqlVales;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 查询逻辑解析
/// </summary>
/// <param name="source"></param>
/// <param name="logic">查询逻辑</param>
public class LogicVisitor(IFieldProvider source, Logic logic)
    : VisitorBase(source)
{
    #region 配置
    /// <summary>
    /// 逻辑
    /// </summary>
    private Logic _logic = logic;
    /// <summary>
    /// 逻辑
    /// </summary>
    public Logic Logic
    {
        get => _logic;
        // 重置,以便复用
        set => _logic = value;
    }
    #endregion
    #region Visit
    /// <inheritdoc/>
    protected override void CheckBinary(ExpressionType op, Expression left, Expression right)
        => CheckLogicByBinary(ref _logic, op, left, right);
    /// <inheritdoc/>
    protected override void CheckUnary(ExpressionType op, Expression expression)
        => CheckUnaryLogic(ref _logic, op, expression);
    /// <inheritdoc/>
    protected override void CheckMethodCall(MethodCallExpression method)
        => CheckLogicByMethodCall(_logic, method);
    /// <inheritdoc/>
    protected override void CheckMember(MemberExpression member)
        => CheckLogicByMember(_logic, member);
    #endregion
    #region Logic
    /// <summary>
    /// 抽取逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <param name="not">否定</param>
    /// <exception cref="NotSupportedException"></exception>
    private void CheckLogic(ref Logic logic, Expression expression, bool not = false)
    {
        if (expression is BinaryExpression binary)
            CheckLogicByBinary(ref logic, binary.NodeType, binary.Left, binary.Right, not);
        else if (expression is UnaryExpression unary)
            CheckUnaryLogic(ref logic, unary.NodeType, unary.Operand, not);
        else if (expression is MemberExpression member)
            CheckLogicByMember(logic, member, not);
        else if (expression is MethodCallExpression method)
            CheckLogicByMethodCall(logic, method, not);
        //else if (expression is LambdaExpression lambda)
        //    VisitLambda(lambda);
        else
            throw new NotSupportedException("不支持的表达式" + expression.NodeType.ToString());
    }
    /// <summary>
    /// 二元逻辑处理
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="op">操作</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <param name="not">否定</param>
    private void CheckLogicByBinary(ref Logic logic, ExpressionType op, Expression left, Expression right, bool not = false)
    {
        switch (op)
        {
            case ExpressionType.AndAlso:
                Logic and = new AndLogic();
                CheckLogic(ref and, left);
                CheckLogic(ref and, right);
                if (not)
                    and = and.Not();
                logic = and.MergeTo(logic);
                break;
            case ExpressionType.OrElse:
                Logic or = new OrLogic();
                CheckLogic(ref or, left);
                CheckLogic(ref or, right);
                if (not)
                    or = or.Not();
                logic = or.MergeTo(logic);
                break;
            default:
                CheckCompareLogic(logic, left, op, right, not);
                break;
        }
    }
    /// <summary>
    /// 一元逻辑处理
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="op">操作</param>
    /// <param name="expression">表达式</param>
    /// <param name="not">否定</param>
    private void CheckUnaryLogic(ref Logic logic, ExpressionType op, Expression expression, bool not = false)
    {
        switch (op)
        {
            case ExpressionType.Not:
                CheckLogic(ref logic, expression, !not);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 处理字段逻辑(bool类型字段)
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="member"></param>
    /// <param name="not">否定</param>
    private void CheckLogicByMember(Logic logic, MemberExpression member, bool not = false)
    {
        var symbol = not ? CompareSymbol.NotEqual : CompareSymbol.Equal;
        if (_source.GetFieldByMember(member) is IField field)
            logic.AddLogic(new CompareLogic(field, symbol, SqlValue.True));
    }
    /// <summary>
    /// 处理方法调用逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="method"></param>
    /// <param name="not">否定</param>
    /// <exception cref="NotSupportedException"></exception>
    private void CheckLogicByMethodCall(Logic logic, MethodCallExpression method, bool not = false)
    {
        var target = method.Object;
        var arguments = method.Arguments;
        switch (method.Method.Name)
        {
            case "Equals":
                CheckCompareLogicByArgument(logic, target, arguments, not ? CompareSymbol.NotEqual : CompareSymbol.Equal);
                break;
            case "Contains":
                CheckInLogic(logic, target, arguments, not);
                break;
            //case "StartsWith":
            //    break;
            //case "EndsWith":
            //    break;
            default:
                throw new NotSupportedException("不支持函数" + method.Method.Name);
        }
    }
    /// <summary>
    /// 处理方法调用
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="target"></param>
    /// <param name="arguments"></param>
    /// <param name="symbol">操作符</param>
    private void CheckCompareLogicByArgument(Logic logic, Expression? target, IList<Expression> arguments, CompareSymbol symbol)
    {
        var argumentCount = arguments.Count;
        if (target is null)
        {
            if (argumentCount == 2)
                CheckCompareLogic(logic, arguments[0], symbol, arguments[1]);
        }
        else if (argumentCount == 1)
        {
            CheckCompareLogic(logic, target, symbol, arguments[0]);
        }
    }
    #endregion
    #region CheckInLogic
    /// <summary>
    /// In逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="target"></param>
    /// <param name="arguments"></param>
    /// <param name="not">否定</param>
    private void CheckInLogic(Logic logic, Expression? target, IList<Expression> arguments, bool not = false)
    {
        var argumentCount = arguments.Count;
        if (target is null)
        {
            if (argumentCount == 2)
                CheckInLogic(logic, arguments[1], arguments[0], not);
        }
        else if (argumentCount == 1)
        {
            CheckInLogic(logic, arguments[0], target, not);
        }
    }
    /// <summary>
    /// In逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <param name="not">否定</param>
    private void CheckInLogic(Logic logic, Expression left, Expression right, bool not = false)
    {
        switch (right.NodeType)
        {
            case ExpressionType.MemberAccess:
                if (right is MemberExpression member)
                {
                    if (_source.GetFieldByExpression(left) is IField field)
                    {
                        if (member.Expression is ConstantExpression && Expression.Lambda(member).Compile().DynamicInvoke() is IEnumerable memberObjects)
                        {
                            var sqlValue = SqlValueService.Values(left.Type, memberObjects);
                            logic.AddLogic(new CompareLogic(field, not ? CompareSymbol.NotIn : CompareSymbol.In, sqlValue));
                        }
                        else
                        {
                            logic.AddLogic(new CompareLogic(field, not ? CompareSymbol.NotIn : CompareSymbol.In, Parameter.Use(member.Member.Name)));
                        }
                    }
                }
                break;
            case ExpressionType.Constant:
                if (right is ConstantExpression constant && constant.Value is IEnumerable objects)
                {
                    if (_source.GetFieldByExpression(left) is IField field)
                    {
                        var sqlValue = SqlValueService.Values(left.Type, objects);
                        logic.AddLogic(new CompareLogic(field, not ? CompareSymbol.NotIn : CompareSymbol.In, sqlValue));
                    }   
                }
                break;
            case ExpressionType.Convert:
                if (right is UnaryExpression unary)
                    CheckInLogic(logic, left, unary.Operand);
                break;
            default:
                break;
        }
    }
    #endregion
    #region CheckCompareLogic
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="left">左</param>
    /// <param name="type"></param>
    /// <param name="right">右</param>
    /// <param name="not">否定</param>
    private void CheckCompareLogic(Logic logic, Expression left, ExpressionType type, Expression right, bool not = false)
    {
        if (_source.GetCompareFieldByExpression(left) is ICompareView leftField)
            CheckCompareLogic(logic, leftField, SymbolManager.GetCompareSymbol(type), right, not);
    }
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="left">左</param>
    /// <param name="symbol">操作符</param>
    /// <param name="right">右</param>
    /// <param name="not">否定</param>
    private void CheckCompareLogic(Logic logic, Expression left, CompareSymbol symbol, Expression right, bool not = false)
    {
        if (_source.GetCompareFieldByExpression(left) is ICompareView leftField)
            CheckCompareLogic(logic, leftField, symbol, right, not);
    }
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="left">左</param>
    /// <param name="symbol">操作符</param>
    /// <param name="right">右</param>
    /// <param name="not">否定</param>
    private void CheckCompareLogic(Logic logic, ICompareView left, CompareSymbol symbol, Expression right, bool not = false)
    {
        if (not)
            symbol = symbol._not;
        switch (right.NodeType)
        {
            case ExpressionType.MemberAccess:
                if (right is MemberExpression member)
                {
                    if (member.Expression is ConstantExpression)
                    {
                        //如果是常量,特殊处理
                        logic.AddLogic(GetConstantLogic(left, symbol, Expression.Lambda(member).Compile().DynamicInvoke()));
                        return;
                    }
                    break;
                }
                break;
            case ExpressionType.Constant:
                if (right is ConstantExpression constant)
                {
                    //如果是常量,特殊处理
                    logic.AddLogic(GetConstantLogic(left, symbol, constant.Value));
                    return;
                }
                break;
            case ExpressionType.Convert:
                if (right is UnaryExpression unary)
                {
                    CheckCompareLogic(logic, left, symbol, unary.Operand, false);
                    return;
                }
                break;
            default:
                break;
        }
        if (_source.GetCompareFieldByExpression(right) is ICompareView rightField)
        {
            logic.AddLogic(new CompareLogic(left, symbol, rightField));
        }
    }
    #endregion
    #region 静态方法
    /// <summary>
    /// 获取sql值
    /// </summary>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static ISqlValue GetSqlValue(object? value)
    {
        if (value is null)
            return SqlValue.Null;
        return SqlValueService.From(value.GetType(), value);
    }
    /// <summary>
    /// 常量逻辑
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="symbol">操作符</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AtomicLogic GetConstantLogic(ICompareView field, CompareSymbol symbol, object? value)
    {
        if (value is null)
        {
            if (symbol == CompareSymbol.NotEqual)
                return new NotNullLogic(field);
            else
                return new IsNullLogic(field);
        }
        return new CompareLogic(field, symbol, SqlValueService.From(value.GetType(), value));
    } 
    #endregion
}
