using ShadowSql.Aggregates;
using ShadowSql.FieldInfos;

namespace ShadowSql.Identifiers;

/// <summary>
/// 列对象基类
/// </summary>
/// <param name="name"></param>
public abstract class ColumnBase(string name)
    : IdentifierBase(name)
{
    /// <summary>
    /// 聚合别名
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public virtual IAggregateFieldAlias AggregateAs(string aggregate, string alias)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountAliasFieldInfo(alias, (IFieldView)this);
        return new AggregateAliasFieldInfo(aggregate, (IFieldView)this, alias);
    }
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public virtual IAggregateField AggregateTo(string aggregate)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountFieldInfo((IFieldView)this);
        return new AggregateFieldInfo(aggregate, (IFieldView)this);
    }
    /// <summary>
    /// 获取别名列
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public virtual IFieldAlias As(string alias)
        => new AliasFieldInfo(alias, (IFieldView)this);
}