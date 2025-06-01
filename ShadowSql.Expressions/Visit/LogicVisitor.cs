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
    /// <param name="op"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    protected override void CheckBinary(ExpressionType op, Expression left, Expression right)
        => CheckLogicByBinary(ref _logic, op, left, right);
    /// <summary>
    /// 处理一元运算
    /// </summary>
    /// <param name="op"></param>
    /// <param name="expression"></param>
    protected override void CheckUnary(ExpressionType op, Expression expression)
        => CheckUnaryLogic(ref _logic, op, expression);
    /// <summary>
    /// 处理方法调用
    /// </summary>
    /// <param name="method"></param>
    protected override void CheckMethodCall(MethodCallExpression method)
        => CheckLogicByMethodCall(_logic, method);
    /// <summary>
    /// 处理字段逻辑(bool类型字段)
    /// </summary>
    /// <param name="member"></param>
    protected override void CheckMember(MemberExpression member)
        => CheckLogicByMember(_logic, member);
    #endregion
    #region Logic
    /// <summary>
    /// 抽取逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="expression"></param>
    /// <param name="not"></param>
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
    /// <param name="logic"></param>
    /// <param name="op"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="not"></param>
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
    /// <param name="logic"></param>
    /// <param name="op"></param>
    /// <param name="expression"></param>
    /// <param name="not"></param>
    private void CheckUnaryLogic(ref Logic logic, ExpressionType op, Expression expression, bool not = false)
    {
        switch (op)
        {
            case ExpressionType.Not:
                CheckNotLogic(ref _logic, expression, not);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 处理字段逻辑(bool类型字段)
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="member"></param>
    /// <param name="not"></param>
    private void CheckLogicByMember(Logic logic, MemberExpression member, bool not = false)
    {
        var symbol = not ? CompareSymbol.NotEqual : CompareSymbol.Equal;
        if (_source.GetFieldByMember(member) is IField field)
            logic.AddLogic(new CompareLogic(field, symbol, SqlValue.True));
    }
    /// <summary>
    /// 处理方法调用逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="method"></param>
    /// <param name="not"></param>
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
    private void CheckNotLogic(ref Logic logic, Expression expression, bool not = false)
    {
        if (not)
            CheckLogic(ref logic, expression, false);
        else
            CheckLogic(ref logic, expression, true);
    }
    #region CheckInLogic
    /// <summary>
    /// In逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="target"></param>
    /// <param name="arguments"></param>
    /// <param name="not"></param>
    private void CheckInLogic(Logic logic, Expression? target, IList<Expression> arguments, bool not = false)
    {
        var argumentCount = arguments.Count;
        if (target is null)
        {
            if (argumentCount == 2 && _source.GetFieldByExpression(arguments[1]) is IField field)
                CheckInLogic(logic, field, arguments[0], not);
        }
        else if (argumentCount == 1 && _source.GetFieldByExpression(arguments[0]) is IField field)
        {
            CheckInLogic(logic, field, target, not);
        }
    }
    /// <summary>
    /// In逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="field"></param>
    /// <param name="right"></param>
    /// <param name="not"></param>
    private static void CheckInLogic(Logic logic, IField field, Expression right, bool not = false)
    {
        if (right is MemberExpression parameter)
        {
            logic.AddLogic(new CompareLogic(field, not ? CompareSymbol.NotIn : CompareSymbol.In, Parameter.Use(parameter.Member.Name)));
        }
        else if (right is ConstantExpression constant && constant.Value is IEnumerable objects)
        {
            var sqlValue = SqlValueService.Values(constant.Type, objects);
            logic.AddLogic(new CompareLogic(field, not ? CompareSymbol.NotIn : CompareSymbol.In, sqlValue));
        }
    }
    #endregion
    #region CheckCompareLogic
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="left"></param>
    /// <param name="type"></param>
    /// <param name="right"></param>
    /// <param name="not"></param>
    private void CheckCompareLogic(Logic logic, Expression left, ExpressionType type, Expression right, bool not = false)
    {
        if (_source.GetCompareFieldByExpression(left) is ICompareView leftField)
            CheckCompareLogic(logic, leftField, SymbolManager.GetCompareSymbol(type), right, not);
    }
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="left"></param>
    /// <param name="symbol"></param>
    /// <param name="right"></param>
    /// <param name="not"></param>
    private void CheckCompareLogic(Logic logic, Expression left, CompareSymbol symbol, Expression right, bool not = false)
    {
        if (_source.GetCompareFieldByExpression(left) is ICompareView leftField)
            CheckCompareLogic(logic, leftField, symbol, right, not);
    }
    /// <summary>
    /// 比较逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="left"></param>
    /// <param name="symbol"></param>
    /// <param name="right"></param>
    private void CheckCompareLogic(Logic logic, ICompareView left, CompareSymbol symbol, Expression right, bool not = false)
    {
        if (not)
            symbol = symbol._not;
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
