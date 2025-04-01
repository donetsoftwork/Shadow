using ShadowSql.Aggregates;
using ShadowSql.Identifiers;

namespace ShadowSql.FieldInfos;

/// <remarks>
/// 列字段信息基类
/// </remarks>
/// <param name="name"></param>
public abstract class FieldInfoBase(string name)
    : ColumnBase(name)
{
    #region 功能
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public override IAggregateField AggregateTo(string aggregate)
    {
        return new AggregateFieldInfo(aggregate, _name);
    }
    /// <summary>
    /// 聚合别名
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public override IAggregateFieldAlias AggregateAs(string aggregate, string alias = "")
    {
        return new AggregateAliasFieldInfo(aggregate, _name, alias);
    }
    /// <summary>
    /// 生成别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public override IFieldAlias As(string alias)
    {
        return new AliasFieldInfo(_name, alias);
    }
    #endregion
}
