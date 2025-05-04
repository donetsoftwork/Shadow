using ShadowSql.Identifiers;
using ShadowSql.SqlVales;

namespace ShadowSql.Insert;

/// <summary>
/// 被插入多值
/// </summary>
/// <param name="column"></param>
/// <param name="values"></param>
public class InsertValues(IColumn column, params ISqlValue[] values)
    : IInsertValues
{
    #region 配置
    private readonly IColumn _column = column;
    private readonly ISqlValue[] _values = values;

    /// <summary>
    /// 左边列
    /// </summary>
    public IColumn Column
        => _column;
    /// <summary>
    /// 右边值(也可以是列)
    /// </summary>
    public ISqlValue[] Values
        => _values;
    #endregion
}