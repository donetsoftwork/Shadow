using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段别名信息
/// </summary>
public class AggregateAliasFieldInfo(ICompareField field, string aggregate, string alias = "")
    : AggregateFieldInfoBase(aggregate, field), IAggregateFieldAlias
{
    #region 配置
    /// <summary>
    /// 别名
    /// </summary>
    private readonly string _alias = alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => CheckAlias(_aggregate, _target.ViewName, _alias);
    #endregion    
    /// <summary>
    /// 检查别名
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="columnName"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static string CheckAlias(string aggregate, string columnName, string alias)
        => string.IsNullOrWhiteSpace(alias) ? aggregate + columnName : alias;
    #region IFieldView
    string IView.ViewName
        => Alias;
    IColumn IFieldView.ToColumn()
        => Column.Use(Alias);
    IFieldAlias IFieldView.As(string alias)
        => new AggregateAliasFieldInfo(_target, _aggregate, alias);
    bool IMatch.IsMatch(string name)
        => Identifier.Match(name, Alias);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
        engine.ColumnAs(sql, Alias);
    }
    #endregion
}
