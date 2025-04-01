using ShadowSql.Aggregates;

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
    public abstract IAggregateFieldAlias AggregateAs(string aggregate, string alias);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public abstract IAggregateField AggregateTo(string aggregate);
    /// <summary>
    /// 获取别名列
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public abstract IFieldAlias As(string alias);
}