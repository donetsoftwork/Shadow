namespace ShadowSql.Identifiers;

/// <summary>
/// 成员列
/// </summary>
/// <param name="memberName"></param>
/// <param name="columnName"></param>
public class MemberColumn(string memberName, string columnName)
    : ColumnBase(columnName), IColumn
{
    #region 配置
    private readonly string _memberName = memberName;
    /// <inheritdoc/>
    public string MemberName 
        => _memberName;
    /// <inheritdoc/>
    string IMember.MemberName
        => _memberName;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => this;
    #endregion
}
