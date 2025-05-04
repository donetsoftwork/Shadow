using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SqlVales;

namespace ShadowSql.Assigns;

/// <summary>
/// 赋值操作
/// </summary>
public interface IAssignOperation : IAssignInfo, ISqlEntity
{
    /// <summary>
    /// 左边列
    /// </summary>
    IAssignView Column { get; }
    /// <summary>
    /// 右边值(也可以是列)
    /// </summary>
    ISqlValue Value { get; }
    /// <summary>
    /// 赋值操作符(默认Equal)
    /// </summary>
    public AssignSymbol Assign { get; }
}
