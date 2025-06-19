using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <inheritdoc />
public class DistinctCountFieldInfo(ICompareField field)
    : DistinctCountFieldInfoBase(field), IAggregateField
{
    /// <inheritdoc/>
    string IAggregateField.TargetName
        => _target.ViewName;
    /// <inheritdoc/>
    IAggregateFieldAlias IAggregateField.As(string alias)
        => new DistinctCountAliasFieldInfo(_target, alias);
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(AggregateConstants.Count)
            .Append("(DISTINCT ");
        _target.Write(engine, sql);
        sql.Append(')');
    }
    #endregion
}
