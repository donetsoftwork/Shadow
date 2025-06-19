using ShadowSql.Aggregates;
using ShadowSql.Expressions.Services;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 筛选解析
/// </summary>
public class SelectVisitor(IFieldProvider source, List<IFieldView> fields)
    : VisitorBase(source)
{
    /// <summary>
    /// 筛选解析
    /// </summary>
    /// <param name="source"></param>
    public SelectVisitor(IFieldProvider source)
        : this(source, [])
    {
    }
    #region 配置
    private readonly List<IFieldView> _fields = fields;
    /// <summary>
    /// 字段列表
    /// </summary>
    public List<IFieldView> Fields 
        => _fields;
    #endregion
    /// <inheritdoc/>
    protected override void CheckMember(MemberExpression member)
    {
        var list = _source.SelectFieldsByMember(member);
        foreach (var item in list)
            _fields.Add(item);
    }
    /// <inheritdoc/>
    protected override void CheckAssignment(Expression expression, MemberInfo info)
    {
        var list = _source.SelectFieldsByAssignment(expression, info);
        foreach (var item in list)
            _fields.Add(item);
    }
    /// <inheritdoc/>
    protected override void CheckMethodCall(MethodCallExpression method)
    {
        if (_source.GetCompareFieldByMethodCall(method) is ICompareView field)
        {
            if (field is IAggregateField aggregateField)
            {
                string alias = aggregateField.TargetName;
                if (!string.IsNullOrEmpty(alias))
                    _fields.Add(aggregateField.As(alias));
                return;
            }
            _fields.Add(field.As(method.Method.Name));
        }
    }
    #region SelectParameter
    /// <summary>
    /// 作为参数
    /// </summary>
    /// <param name="memberInfo"></param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static AliasFieldInfo SelectParameter(MemberInfo memberInfo, string aliasName)
        => new(Parameter.Use(memberInfo.Name), aliasName);
    /// <summary>
    /// 作为参数
    /// </summary>
    /// <param name="memberInfo"></param>
    /// <returns></returns>
    public static AliasFieldInfo SelectParameter(MemberInfo memberInfo)
        => new(Parameter.Use(memberInfo.Name), memberInfo.Name);
    /// <summary>
    /// 选择常量
    /// </summary>
    /// <param name="constant"></param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static AliasFieldInfo SelectValue(ConstantExpression constant, string aliasName)
        => new(SqlValueService.From(constant.Type, constant.Value), aliasName);
    #endregion
    /// <summary>
    /// 筛选字段
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static IFieldView SelectField(IField field, string aliasName)
    {
        if (field.IsMatch(aliasName))
            return field;
        else
            return field.As(aliasName);
    }
}
