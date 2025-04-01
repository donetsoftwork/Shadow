using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 子查询作为字段
/// </summary>
/// <param name="target"></param>
public class SingleField(ISingleSelect target)
    : IFieldView
{
    /// <summary>
    /// 拼写sql(子查询需要加小括号)
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {

        sql.Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
    }
    /// <summary>
    /// 子查询字段别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public SingleFieldAlias As(string alias)
        => new(_target, alias);
    #region 配置
    /// <summary>
    /// 被包裹片段
    /// </summary>
    private readonly ISingleSelect _target = target;
    /// <summary>
    /// 被包裹片段
    /// </summary>
    public ISingleSelect Target
        => _target;
    #endregion
    #region IFieldView
    string IView.ViewName
        => _target.SingleField.ViewName;
    IColumn IFieldView.ToColumn()
        => _target.SingleField.ToColumn();

    bool IMatch.IsMatch(string name)
        => _target.SingleField.IsMatch(name);
    IFieldAlias IFieldView.As(string alias)
        => As(alias);
    #endregion
}
