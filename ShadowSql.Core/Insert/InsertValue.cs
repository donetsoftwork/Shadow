using ShadowSql.Identifiers;
using ShadowSql.SqlVales;

namespace ShadowSql.Insert;

/// <summary>
/// 被插入单值
/// </summary>
/// <param name="column">列</param>
/// <param name="value">值</param>
public class InsertValue(IColumn column, ISqlValue value)
    : IInsertValue
{
    #region 配置
    private readonly IColumn _column = column;
    private readonly ISqlValue _value = value;
    
    /// <summary>
    /// 左边列
    /// </summary>
    public IColumn Column
        => _column;
    /// <summary>
    /// 右边值(也可以是列)
    /// </summary>
    public ISqlValue Value
        => _value;
    #endregion
}
