using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 子查询字段别名
/// </summary>
/// <param name="target"></param>
/// <param name="alias"></param>
public class SingleFieldAlias(ISingleSelect target, string alias)
    : SqlAlias<ISingleSelect>(target, alias), IFieldAlias
{
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
        engine.ColumnAs(sql, _name);
    }
    #region IFieldView
    string IView.ViewName
        => _name;
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);

    IFieldAlias IFieldView.As(string alias)
        =>new SingleFieldAlias(_target, alias);
    #endregion
}
