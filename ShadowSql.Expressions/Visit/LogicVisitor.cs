using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Expressions.Services;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SqlVales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 查询逻辑解析
/// </summary>
/// <param name="source"></param>
/// <param name="logic"></param>
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
    /// <summary>
    /// 处理两元运算
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override Expression VisitBinary(BinaryExpression node)
    {
        CheckLogicByBinary(ref _logic, node);
        return node;
    }
    private void CheckLogicByBinary(ref Logic logic, BinaryExpression node)
    {
        var type = node.NodeType;
        switch (type)
        {
            case ExpressionType.AndAlso:
                Logic and = new AndLogic();
                CheckLogic(ref and, node.Left);
                CheckLogic(ref and, node.Right);
                logic = and.MergeTo(logic);
                break;
            case ExpressionType.OrElse:
                Logic or = new OrLogic();
                CheckLogic(ref or, node.Left);
                CheckLogic(ref or, node.Right);
                logic = or.MergeTo(logic);
                break;
            case ExpressionType.Not:
                break;
            default:
                CheckCompareLogic(logic, node.Left, type, node.Right);
                break;
        }
    }
    /// <summary>
    /// 处理方法调用
    /// </summary>
    /// <param name="method"></param>
    protected override void CheckMethodCall(MethodCallExpression method)
    {
        CheckLogicByMethodCall(_logic, method);
    }
    private void CheckLogicByMethodCall(Logic logic, MethodCallExpression method)
    {
        var target = method.Object;
        var arguments = method.Arguments;
        switch (method.Method.Name)
        {
            case "Equals":
                CheckCompareLogicByArgument(logic, target, arguments, CompareSymbol.Equal);
                break;
            case "Contains":
                CheckInLogic(logic, target, arguments);
                break;
            //case "StartsWith":
            //    break;
            //case "EndsWith":
            //    break;
            default:
                throw new NotSupportedException("不支持函数" + method.Method.Name);
        }
    }

    private void CheckLogic(ref Logic logic, Expression expression)
    {
        if (expression is BinaryExpression binary)
            CheckLogicByBinary(ref logic, binary);
        else if (expression is MemberExpression member)
            CheckLogicByMember(logic, member);
        else if (expression is MethodCallExpression method)
            CheckLogicByMethodCall(logic, method);
        //else if (expression is UnaryExpression unary)
        //    VisitUnary(unary);
        //else if (expression is LambdaExpression lambda)
        //    VisitLambda(lambda);
        else
            throw new NotSupportedException("不支持的表达式" + expression.NodeType.ToString());

    }
    /// <summary>
    /// 处理bool字段逻辑
    /// </summary>
    /// <param name="member"></param>
    protected override void CheckMember(MemberExpression member)
        => CheckLogicByMember(_logic, member);
    /// <summary>
    /// 处理bool字段逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="member"></param>
    private void CheckLogicByMember(Logic logic, MemberExpression member)
    {
        if(_source.GetFieldByMember(member) is IField field)
            logic.AddLogic(new CompareLogic(field, CompareSymbol.Equal, SqlValue.True));
    }
    /// <summary>
    /// 处理方法调用
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="target"></param>
    /// <param name="arguments"></param>
    /// <param name="symbol"></param>
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
    /// 处理IN逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="target"></param>
    /// <param name="arguments"></param>
    private void CheckInLogic(Logic logic, Expression? target, IList<Expression> arguments)
    {
        var argumentCount = arguments.Count;
        if (target is null)
        {
            if (argumentCount == 2 && _source.GetFieldByExpression(arguments[1]) is IField field)
                CheckInLogic(logic, field, arguments[0]);
        }
        else if (argumentCount == 1 && _source.GetFieldByExpression(arguments[0]) is IField field)
        {
            CheckInLogic(logic, field, target);
        }
    }
    /// <summary>
    /// In逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="field"></param>
    /// <param name="right"></param>
    private static void CheckInLogic(Logic logic, IField field, Expression right)
    {
        if (right is MemberExpression parameter)
        {
            logic.AddLogic(new CompareLogic(field, CompareSymbol.In, Parameter.Use(parameter.Member.Name)));
        }
        else if (right is ConstantExpression constant && constant.Value is IEnumerable objects)
        {
            var sqlValue = SqlValueService.Values(constant.Type, objects);
            logic.AddLogic(new CompareLogic(field, CompareSymbol.In, sqlValue));
        }
    }

    #endregion
    #region CheckCompareLogic
    private void CheckCompareLogic(Logic logic, Expression left, ExpressionType type, Expression right)
    {
        if (_source.GetCompareFieldByExpression(left) is ICompareView leftField)
            CheckCompareLogic(logic, leftField, SymbolManager.GetCompareSymbol(type), right);
    }
    private void CheckCompareLogic(Logic logic, Expression left, CompareSymbol symbol, Expression right)
    {
        if (_source.GetCompareFieldByExpression(left) is ICompareView leftField)
            CheckCompareLogic(logic, leftField, symbol, right);
    }
    /// <summary>
    /// 抽取逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="left"></param>
    /// <param name="symbol"></param>
    /// <param name="right"></param>
    private void CheckCompareLogic(Logic logic, ICompareView left, CompareSymbol symbol, Expression right)
    {
        if (right is ConstantExpression constant)
        {
            //如果是常量,特殊处理
            logic.AddLogic(GetConstantLogic(left, symbol, constant));
        }
        else if (_source.GetCompareFieldByExpression(right) is ICompareView rightField)
        {
            logic.AddLogic(new CompareLogic(left, symbol, rightField));
        }
    }
    #endregion
    #region 静态方法
    /// <summary>
    /// 获取sql值
    /// </summary>
    /// <param name="constant"></param>
    /// <returns></returns>
    public static ISqlValue GetSqlValue(ConstantExpression constant)
    {
        object? value = constant.Value;
        if (value is null)
            return SqlValue.Null;
        return SqlValueService.From(constant.Type, value);
    }
    /// <summary>
    /// 常量逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="symbol"></param>
    /// <param name="constant"></param>
    /// <returns></returns>
    public static AtomicLogic GetConstantLogic(ICompareView field, CompareSymbol symbol, ConstantExpression constant)
    {
        var value = constant.Value;
        if (value is null)
        {
            if (symbol == CompareSymbol.NotEqual)
                return new NotNullLogic(field);
            else
                return new IsNullLogic(field);
        }
        return new CompareLogic(field, symbol, SqlValueService.From(constant.Type, value));
    } 
    #endregion
}
