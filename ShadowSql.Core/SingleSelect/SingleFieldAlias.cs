using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 子查询字段别名
/// </summary>
/// <param name="select">筛选</param>
/// <param name="aliasName">别名</param>
public class SingleFieldAlias(ISingleSelect select, string aliasName)
    : SqlAlias<ISingleSelect>(select, aliasName), IFieldAlias
{
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
        engine.ColumnAs(sql, _name);
    }
    #region IFieldView
    /// <inheritdoc/>
    string IView.ViewName
        => _name;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
    /// <inheritdoc/>
    IFieldAlias IFieldView.As(string aliasName)
        => new SingleFieldAlias(_target, aliasName);
    #endregion
}
