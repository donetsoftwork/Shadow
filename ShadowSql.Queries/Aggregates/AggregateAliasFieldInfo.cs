using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段别名信息
/// </summary>
/// <param name="original">原始字段</param>
/// <param name="aggregate">聚合</param>
/// <param name="aliasName">别名</param>
public class AggregateAliasFieldInfo(ICompareField original, string aggregate, string aliasName = "")
    : AggregateFieldInfoBase(aggregate, original), IAggregateFieldAlias
{
    #region 配置
    /// <summary>
    /// 别名
    /// </summary>
    private readonly string _alias = aliasName;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => CheckAlias(_aggregate, _original.ViewName, _alias);
    #endregion    
    /// <summary>
    /// 检查别名
    /// </summary>
    /// <param name="aggregate">聚合</param>
    /// <param name="columnName">列名</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static string CheckAlias(string aggregate, string columnName, string aliasName)
        => string.IsNullOrWhiteSpace(aliasName) ? aggregate + columnName : aliasName;
    #region IFieldView
    /// <inheritdoc/>
    string IView.ViewName
        => Alias;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => Column.Use(Alias);
    ///// <inheritdoc/>
    //IFieldAlias IFieldView.As(string aliasName)
    //    => new AggregateAliasFieldInfo(_original, _aggregate, aliasName);
    /// <inheritdoc/>
    bool IMatch.IsMatch(string name)
        => Identifier.Match(name, Alias);
    #endregion
    /// <inheritdoc/>
    IAggregateField IAggregateFieldAlias.ToAggregate()
        => new AggregateFieldInfo(_original, _aggregate);
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        _original.Write(engine, sql);
        sql.Append(')');
        engine.ColumnAs(sql, Alias);
    }
    #endregion
}
