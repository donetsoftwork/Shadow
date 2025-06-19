using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 去重字段统计别名
/// </summary>
public class DistinctCountAliasFieldInfo(ICompareField field, string fieldName/* = "Count"*/)
    : DistinctCountFieldInfoBase(field), IAggregateFieldAlias
{
    #region 配置
    private readonly string _alias = fieldName;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _alias;
    #endregion
    /// <summary>
    /// 匹配
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public bool IsMatch(string fieldName)
        => Identifier.Match(fieldName, _alias);
    #region IFieldView
    /// <inheritdoc/>
    string IView.ViewName
        => _alias;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => Column.Use(_alias);
    /// <inheritdoc/>
    IFieldAlias IFieldView.As(string aliasName)
        => new DistinctCountAliasFieldInfo(_target, aliasName);
    #endregion
    /// <inheritdoc/>
    IAggregateField IAggregateFieldAlias.ToAggregate()
        => new DistinctCountFieldInfo(_target);
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(AggregateConstants.Count)
            .Append("(DISTINCT ");
        _target.Write(engine, sql);
        sql.Append(')');
        engine.ColumnAs(sql, _alias);
    }
    #endregion
}
