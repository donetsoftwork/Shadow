using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 排序解析
/// </summary>
public class OrderByVisitor(IFieldProvider source, List<IOrderAsc> fields)
    : VisitorBase(source)
{
    /// <summary>
    /// 排序解析
    /// </summary>
    /// <param name="source"></param>
    public OrderByVisitor(IFieldProvider source)
        : this(source, [])
    {
    }
    #region 配置
    private readonly List<IOrderAsc> _fields = fields;
    /// <summary>
    /// 字段列表
    /// </summary>
    public List<IOrderAsc> Fields
        => _fields;
    #endregion
    /// <inheritdoc/>
    protected override void CheckMember(MemberExpression member)
    {
        if (_source.GetCompareFieldByExpression(member) is IOrderAsc field)
            _fields.Add(field);
    }
    /// <inheritdoc/>
    protected override void CheckAssignment(Expression expression, MemberInfo info)
    {
        if (_source.GetCompareFieldByExpression(expression) is IOrderAsc field)
            _fields.Add(field);
    }
    /// <inheritdoc/>
    protected override void CheckMethodCall(MethodCallExpression method)
    {
        if (_source.GetCompareFieldByMethodCall(method) is IOrderAsc field)
            _fields.Add(field);
    }
}
