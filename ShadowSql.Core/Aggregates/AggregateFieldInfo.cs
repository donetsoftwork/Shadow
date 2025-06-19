using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <inheritdoc />
public sealed class AggregateFieldInfo(ICompareField target, string aggregate)
    : AggregateFieldInfoBase(aggregate, target), IAggregateField, ISqlEntity
{
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
    }
    /// <inheritdoc/>
    IAggregateFieldAlias IAggregateField.As(string alias)
        => new AggregateAliasFieldInfo(_target, _aggregate, alias);
    /// <inheritdoc/>
    string IAggregateField.TargetName
        => _target.ViewName;
}
