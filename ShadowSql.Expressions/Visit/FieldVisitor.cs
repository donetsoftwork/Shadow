using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 筛选解析基类
/// </summary>
public class FieldVisitor(IFieldProvider source, ICollection<IField> fields)
    : VisitorBase(source)
{
    #region 配置
    private readonly ICollection<IField> _fields = fields;
    /// <summary>
    /// 字段列表
    /// </summary>
    public IEnumerable<IField> Fields
        => _fields;
    #endregion
    /// <inheritdoc/>
    protected override void CheckMember(MemberExpression member)
    {
        foreach (var field in _source.GetFieldsByMember(member))
            _fields.Add(field);
    }
    /// <inheritdoc/>
    protected override void CheckAssignment(Expression expression, MemberInfo info)
    {
        if (_source.GetFieldByExpression(expression) is IField field)
        {
            var name = info.Name;
            if (field.IsMatch(name))
                _fields.Add(field);
            else
                throw new ArgumentException("不支持重命名字段");
        }
    }    
}
