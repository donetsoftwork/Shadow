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
    /// <summary>
    /// 处理属性
    /// </summary>
    /// <param name="member"></param>
    protected override void CheckMember(MemberExpression member)
    {
        var list = _source.SelectFieldsByMember(member);
        foreach (var item in list)
            _fields.Add(item);
    }
    /// <summary>
    /// 处理赋值
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="info"></param>
    protected override void CheckAssignment(Expression expression, MemberInfo info)
    {
        var list = _source.SelectFieldsByAssignment(expression, info);
        foreach (var item in list)
            _fields.Add(item);
    }
    /// <summary>
    /// 处理方法调用
    /// </summary>
    /// <param name="method"></param>
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
    /// <param name="member"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static AliasFieldInfo SelectParameter(MemberInfo member, string alias)
        => new(Parameter.Use(member.Name), alias);
    /// <summary>
    /// 作为参数
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public static AliasFieldInfo SelectParameter(MemberInfo member)
        => new(Parameter.Use(member.Name), member.Name);
    /// <summary>
    /// 选择常量
    /// </summary>
    /// <param name="constant"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static AliasFieldInfo SelectValue(ConstantExpression constant, string alias)
        => new(SqlValueService.From(constant.Type, constant.Value), alias);
    #endregion
    /// <summary>
    /// 筛选字段
    /// </summary>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IFieldView SelectField(IField field, string alias)
    {
        if (field.IsMatch(alias))
            return field;
        else
            return field.As(alias);
    }
}
