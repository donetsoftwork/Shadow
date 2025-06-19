using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 子查询作为字段
/// </summary>
/// <param name="select">筛选</param>
public class SingleField(ISingleSelect select)
    : IFieldView
{
    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
    }
    /// <summary>
    /// 子查询字段别名
    /// </summary>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public SingleFieldAlias As(string aliasName)
        => new(_target, aliasName);
    #region 配置
    /// <summary>
    /// 被包裹片段
    /// </summary>
    private readonly ISingleSelect _target = select;
    /// <summary>
    /// 被包裹片段
    /// </summary>
    public ISingleSelect Target
        => _target;
    #endregion
    #region IFieldView
    /// <inheritdoc/>
    string IView.ViewName
        => _target.SingleField.ViewName;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => _target.SingleField.ToColumn();
    /// <inheritdoc/>
    bool IMatch.IsMatch(string name)
        => _target.SingleField.IsMatch(name);
    /// <inheritdoc/>
    IFieldAlias IFieldView.As(string aliasName)
        => As(aliasName);
    #endregion
}
