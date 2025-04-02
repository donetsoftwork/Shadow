using ShadowSql.Identifiers;

namespace ShadowSql.FieldInfos;

/// <remarks>
/// 列字段信息基类
/// </remarks>
/// <param name="name"></param>
public abstract class FieldInfoBase(string name)
    : ColumnBase(name)
{
    //#region 功能
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <param name="aggregate"></param>
    ///// <returns></returns>
    //public override IAggregateField AggregateTo(string aggregate)
    //{
    //    if (AggregateConstants.MatchCount(aggregate))
    //        return new DistinctCountFieldInfo((IFieldView)this);
    //    return new AggregateFieldInfo(aggregate, (IFieldView)this);
    //}
    ///// <summary>
    ///// 聚合别名
    ///// </summary>
    ///// <param name="aggregate"></param>
    ///// <param name="alias"></param>
    ///// <returns></returns>
    //public override IAggregateFieldAlias AggregateAs(string aggregate, string alias = "")
    //{
    //    if (AggregateConstants.MatchCount(aggregate))
    //        return new DistinctCountAliasFieldInfo((IField)this, alias);
    //    return new AggregateAliasFieldInfo((IField)this, aggregate, alias);
    //}
    ///// <summary>
    ///// 生成别名
    ///// </summary>
    ///// <param name="alias"></param>
    ///// <returns></returns>
    //public override IFieldAlias As(string alias)
    //    => new AliasFieldInfo((IField)this, alias);
    //#endregion
}
