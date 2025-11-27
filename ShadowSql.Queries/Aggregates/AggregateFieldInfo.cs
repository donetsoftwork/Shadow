using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <inheritdoc />
public sealed class AggregateFieldInfo(ICompareField original, string aggregate)
    : AggregateFieldInfoBase(aggregate, original), IAggregateField, ISqlEntity
{
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        _original.Write(engine, sql);
        sql.Append(')');
    }
    /// <inheritdoc/>
    IAggregateFieldAlias IAggregateField.As(string alias)
        => new AggregateAliasFieldInfo(_original, _aggregate, alias);
    /// <inheritdoc/>
    string IAggregateField.TargetName
        => _original.ViewName;
}
