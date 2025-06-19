using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 别名字段信息
/// </summary>
/// <param name="statement"></param>
/// <param name="aliasName">别名</param>
public class RawFieldAliasInfo(string statement, string aliasName)
     : IdentifierBase(aliasName), IFieldAlias
{
    private readonly string _statement = statement;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _name;
    /// <summary>
    /// 语句
    /// </summary>
    public string Statement
        => _statement;
    /// <inheritdoc/>
    string IView.ViewName
        => _name;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
    /// <inheritdoc/>
    IFieldAlias IFieldView.As(string aliasName)
        => new RawFieldAliasInfo(_statement, aliasName);
    #region ISqlEntity
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_statement);
        engine.ColumnAs(sql, _name);
    }
    #endregion
}
