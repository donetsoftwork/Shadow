using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段别名信息
/// </summary>
public class AggregateAliasFieldInfo
    : AggregateFieldInfoBase, IAggregateFieldAlias, IFieldView
{
    #region 配置
    /// <summary>
    /// 别名
    /// </summary>
    private readonly string _alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => CheckAlias(_aggregate, _target.ViewName, _alias);
    #endregion
    internal AggregateAliasFieldInfo(string aggregate, IFieldView target, string alias)
        : base(aggregate, target)
    {
        _alias = alias;
    }
    /// <summary>
    /// 聚合列
    /// </summary>
    /// <param name="column"></param>
    /// <param name="aggregate"></param>
    /// <param name="alias"></param>
    public AggregateAliasFieldInfo(IColumn column, string aggregate, string alias = "")
        : this(aggregate, column, alias)
    {
    }
    /// <summary>
    /// 聚合字段
    /// </summary>
    /// <param name="field"></param>
    /// <param name="aggregate"></param>
    /// <param name="alias"></param>
    public AggregateAliasFieldInfo(IField field, string aggregate, string alias = "")
        : this(aggregate, field, alias)
    {
    }
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
    => _target.As(alias);
    bool IMatch.IsMatch(string name)
        => Identifier.Match(name, Alias);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
        engine.ColumnAs(sql, Alias);
    }
    #endregion
}
