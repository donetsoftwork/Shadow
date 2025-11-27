using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <inheritdoc />
public class DistinctCountFieldInfo(ICompareField original)
    : DistinctCountFieldInfoBase(original), IAggregateField
{
    /// <inheritdoc/>
    string IAggregateField.TargetName
        => _original.ViewName;
    /// <inheritdoc/>
    IAggregateFieldAlias IAggregateField.As(string alias)
        => new DistinctCountAliasFieldInfo(_original, alias);
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(AggregateConstants.Count)
            .Append("(DISTINCT ");
        _original.Write(engine, sql);
        sql.Append(')');
    }
    #endregion
}
