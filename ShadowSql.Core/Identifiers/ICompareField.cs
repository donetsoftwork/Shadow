using ShadowSql.Fragments;

namespace ShadowSql.Identifiers;

/// <summary>
/// 比较运算字段
/// </summary>
public interface ICompareField : IView, ICompareView, IOrderField, IMember
{
    ///// <summary>
    ///// 聚合(逻辑运算使用)
    ///// </summary>
    ///// <param name="aggregate">聚合</param>
    ///// <returns></returns>
    //IAggregateField AggregateTo(string aggregate);
    ///// <summary>
    ///// 聚合别名(select使用)
    ///// 别名不能直接参与逻辑运算
    ///// </summary>
    ///// <param name="aggregate">聚合</param>
    ///// <param name="alias"></param>
    ///// <returns></returns>
    //IAggregateFieldAlias AggregateAs(string aggregate, string alias);
}


/// <summary>
/// 比较运算字段或表达式
/// </summary>
public interface ICompareView : IOrderAsc, ISqlEntity;
//#if NET7_0_OR_GREATER
//    , System.Numerics.IAdditionOperators<ICompareView, ICompareView, ArithmeticView>
//#endif
//{
//#if NET7_0_OR_GREATER
//    /// <summary>
//    /// +
//    /// </summary>
//    /// <param name="left"></param>
//    /// <param name="right"></param>
//    /// <returns></returns>
//    public static ArithmeticView operator +(ICompareView left, ICompareView right)
//        => left.Add(right);
//#endif
//}
