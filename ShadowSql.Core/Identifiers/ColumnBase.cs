using ShadowSql.Aggregates;
using ShadowSql.FieldInfos;

namespace ShadowSql.Identifiers;

/// <summary>
/// 列对象基类
/// </summary>
/// <param name="columnName">列</param>
public abstract class ColumnBase(string columnName)
    : IdentifierBase(columnName), ICompareField
{
    /// <inheritdoc/>
    public virtual IAggregateFieldAlias AggregateAs(string aggregate, string alias)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountAliasFieldInfo(this, alias);
        return new AggregateAliasFieldInfo(this, aggregate, alias);
    }
    /// <inheritdoc/>
    public virtual IAggregateField AggregateTo(string aggregate)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountFieldInfo(this);
        return new AggregateFieldInfo(this, aggregate);
    }
    /// <inheritdoc/>
    public virtual IFieldAlias As(string aliasName)
        => new AliasFieldInfo(this, aliasName);
    /// <inheritdoc/>
    string IView.ViewName
        => _name;
}