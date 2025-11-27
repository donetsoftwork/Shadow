using Hand.Structural;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 子查询作为字段
/// </summary>
/// <param name="original">原始筛选</param>
public class SingleField(ISingleSelect original)
    : IFieldView
    , IWrapper<ISingleSelect>
{
    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('(');
        _original.Write(engine, sql);
        sql.Append(')');
    }
    /// <summary>
    /// 子查询字段别名
    /// </summary>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public SingleFieldAlias As(string aliasName)
        => new(_original, aliasName);
    #region 配置
    /// <summary>
    /// 原始筛选
    /// </summary>
    private readonly ISingleSelect _original = original;
    /// <summary>
    /// 原始筛选
    /// </summary>
    public ISingleSelect Original
        => _original;
    #endregion
    #region IFieldView
    /// <inheritdoc/>
    string IView.ViewName
        => _original.SingleField.ViewName;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => _original.SingleField.ToColumn();
    /// <inheritdoc/>
    bool IMatch.IsMatch(string name)
        => _original.SingleField.IsMatch(name);
    ///// <inheritdoc/>
    //IFieldAlias IFieldView.As(string aliasName)
    //    => As(aliasName);
    #endregion
}
