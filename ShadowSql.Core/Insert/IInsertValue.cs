using ShadowSql.Identifiers;
using ShadowSql.SqlVales;

namespace ShadowSql.Insert;

/// <summary>
/// 被插入单值
/// </summary>
public interface IInsertValue
{
    /// <summary>
    /// 左边列
    /// </summary>
    IColumn Column { get; }
    /// <summary>
    /// 右边值(也可以是列)
    /// </summary>
    ISqlValue Value { get; }
}

/// <summary>
/// 被插入多值
/// </summary>
public interface IInsertValues
{
    /// <summary>
    /// 左边列
    /// </summary>
    IColumn Column { get; }
    /// <summary>
    /// 右边值(也可以是列)
    /// </summary>
    ISqlValue[] Values { get; }
}



