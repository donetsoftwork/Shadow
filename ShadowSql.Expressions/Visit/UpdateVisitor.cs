using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 更新逻辑解析
/// </summary>
/// <param name="source"></param>
/// <param name="assigns"></param>
public class UpdateVisitor(IFieldProvider source, ICollection<IAssignInfo> assigns)
    : VisitorBase(source)
{
    #region 配置
    private readonly ICollection<IAssignInfo> _assigns = assigns;
    /// <summary>
    /// 赋值信息
    /// </summary>
    public IEnumerable<IAssignInfo> Assigns
        => _assigns;
    #endregion
    #region Visit
    /// <inheritdoc/>
    protected override void CheckMember(MemberExpression member)
    {
        var list = _source.GetFieldsByMember(member);
        foreach (var item in list)
            _assigns.Add(item.Assign());
    }
    /// <inheritdoc/>
    protected override void CheckAssignment(Expression expression, MemberInfo info)
    {
        if (_source.GetFieldByName(info.Name) is IAssignView field
            && _source.GetCompareFieldByExpression(expression) is ICompareView compareField)
        {
            if (compareField == field)
                _assigns.Add(field.Assign());
            else
                _assigns.Add(field.Assign(compareField));
        }
    }
    #endregion
}
