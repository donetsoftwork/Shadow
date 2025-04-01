using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段别名信息
/// </summary>
/// <param name="aggregate"></param>
/// <param name="column"></param>
/// <param name="alias"></param>
public class AggregateAliasFieldInfo(string aggregate, string column, string alias = "")
    : AggregateFieldInfoBase(aggregate, column), IAggregateFieldAlias, IFieldView
{
    /// <summary>
    /// 别名
    /// </summary>
    private readonly string _alias = alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => CheckAlias(_aggregate, _name, _alias);

    /// <summary>
    /// 检查别名
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="columnName"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static string CheckAlias(string aggregate, string columnName, string alias)
        => string.IsNullOrWhiteSpace(alias) ? aggregate + columnName : alias;

    string IView.ViewName
        => Alias;
    IColumn IFieldView.ToColumn()
        => Column.Use(Alias);

    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        engine.Identifier(sql, _name);
        sql.Append(')');
        engine.ColumnAs(sql, Alias);
    }
}
