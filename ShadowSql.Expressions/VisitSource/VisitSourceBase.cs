using ShadowSql.Arithmetic;
using ShadowSql.Expressions.Visit;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.VisitSource;

/// <summary>
/// 数据源基类
/// </summary>
public abstract class VisitSourceBase(Expression entity)
    : IFieldProvider
{
    #region 配置
    /// <summary>
    /// 实体对象占位表达式
    /// </summary>
    protected readonly Expression _entity = entity;
    /// <summary>
    /// 实体对象占位表达式
    /// </summary>
    public Expression Entity
        => _entity;
    /// <summary>
    /// 所有字段
    /// </summary>
    public abstract IEnumerable<IField> Fields { get; }
    #endregion
    #region GetFields
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public virtual IEnumerable<IField> GetFieldsByExpression(Expression expression)
    {
        if (expression == _entity)
            return Fields;
        if (expression is MemberExpression member)
            return GetFieldsByMember(member);
        return [];
    }
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public abstract IEnumerable<IField> GetFieldsByMember(MemberExpression member);
    #endregion
    #region GetField
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public abstract IField? GetFieldByName(string fieldName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public virtual IField? GetFieldByMember(MemberExpression member)
        => TableVisitor.GetField(GetFieldsByMember(member));
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public virtual IField? GetFieldByExpression(Expression expression)
    {
        if (expression is MemberExpression property && GetFieldByMember(property) is IField field)
            return field;
        return null;
    }
    #endregion
    #region IFieldView
    /// <summary>
    /// 从参数中筛选
    /// </summary>
    /// <param name="argument"></param>
    /// <param name="memberInfo"></param>
    /// <returns></returns>
    public virtual IEnumerable<IFieldView> SelectFieldsByAssignment(Expression argument, MemberInfo memberInfo)
    {
        if (argument.NodeType == ExpressionType.MemberAccess && argument is MemberExpression member)
        {
            if (GetFieldByMember(member) is IField field)
                yield return SelectVisitor.SelectField(field, memberInfo.Name);
            else
                yield return SelectVisitor.SelectParameter(member.Member, memberInfo.Name);
        }
        else if (GetCompareFieldByExpression(argument) is ICompareView compare)
        {
            yield return new AliasFieldInfo(compare, memberInfo.Name);
        }
    }
    /// <summary>
    /// 从属性中筛选
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public virtual IEnumerable<IFieldView> SelectFieldsByMember(MemberExpression member)
    {
        var fields = GetFieldsByMember(member).ToArray();
        if (fields.Length > 0)
            return fields;
        return [SelectVisitor.SelectParameter(member.Member)];
    }
    #endregion
    #region ICompareView
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public virtual ICompareView? GetCompareFieldByExpression(Expression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.MemberAccess:
                if (expression is MemberExpression member)
                {
                    if(member.Expression is ConstantExpression)
                        return LogicVisitor.GetSqlValue(Expression.Lambda(member).Compile().DynamicInvoke());
                    if (GetFieldByMember(member) is IField field)
                        return field;
                    return Parameter.Use(member.Member.Name);
                }
                break;
            case ExpressionType.Constant:
                if (expression is ConstantExpression constant)
                    return LogicVisitor.GetSqlValue(constant.Value);
                break;
            case ExpressionType.Call:
                if (expression is MethodCallExpression methodCall)
                    return GetCompareFieldByMethodCall(methodCall);
                break;
            case ExpressionType.Convert:
                if (expression is UnaryExpression unary)
                    return GetCompareFieldByExpression(unary.Operand);
                break;
            default:
                if (expression is BinaryExpression binary)
                {
                    if (GetCompareFieldByExpression(binary.Left) is ICompareView left
                        && GetCompareFieldByExpression(binary.Right) is ICompareView right)
                        return new ArithmeticView(left, SymbolManager.GetArithmeticSymbol(binary.NodeType), right);
                }
                break;
        }
        return null;
    }
    /// <summary>
    /// 从方法调用中获取比较字段
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public virtual ICompareView? GetCompareFieldByMethodCall(MethodCallExpression methodCall)
        => null;
    #endregion
}
