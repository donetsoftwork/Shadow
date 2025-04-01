using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// Or逻辑扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region Or
    #region AtomicLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static TOrLogic OrCore<TOrLogic>(this TOrLogic logic, AtomicLogic other)
        where TOrLogic : Logic, IOrLogic
    {
        logic.AddLogic(other);
        return logic;
    }
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this OrLogic logic, IAndLogic other)
    //    => other.MergeTo(logic);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IAndLogic other, OrLogic logic)
    //    => other.MergeTo(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this OrLogic logic, AndLogic other)
        => other.MergeToOr(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this OrLogic logic, ComplexAndLogic other)
        => other.MergeToOr(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this OrLogic logic, IOrLogic other)
    //    => other.MergeTo(logic);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IOrLogic other, OrLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static OrLogic OrCore(this OrLogic logic, OrLogic other)
        => other.MergeTo(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this OrLogic logic, ComplexOrLogic other)
        => logic.MergeTo(other);
    #endregion
    #endregion
    #region And
    #region AtomicLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this OrLogic or, AtomicLogic other)
    {
        var logic = or.ToAndCore();
        logic.AddLogic(other);
        return logic;
    }
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this OrLogic logic, IAndLogic other)
    //    => other.And(logic);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this IAndLogic other, OrLogic logic)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this OrLogic logic, AndLogic other)
        => other.AndCore(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this OrLogic logic, ComplexAndLogic other)
        => other.AndCore(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this OrLogic logic, IOrLogic other)
    //    => logic.ToAnd()
    //        .And(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this IOrLogic other, OrLogic logic)
    //    => other.ToAnd()
    //        .And(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this OrLogic logic, OrLogic other)
    {
        var preview = logic.Preview();
        if (preview.IsEmpty)
            return other.ToAndCore();
        if (preview.HasSecond)
            return other.MergeToAnd(new ComplexAndLogic().AddOtherCore(logic.MergeTo(new ComplexOrLogic())));
        return other.MergeToAnd(new AndLogic(preview.First));
    }
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this OrLogic logic, ComplexOrLogic other)
    {
        var preview = logic.Preview();
        if (preview.IsEmpty)
            return other.ToAndCore();
        if (preview.HasSecond)
            return other.MergeToAnd(new ComplexAndLogic().AddOtherCore(logic.MergeTo(new ComplexOrLogic())));
        return other.MergeToAnd(new AndLogic(preview.First));
    }
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// not Or为not每个子项的And
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public static AndLogic Not(this OrLogic or)
    {
        var and = new AndLogic();
        and.NotLogics(or._logics);
        return and;
    }
    /// <summary>
    /// Not逻辑简化
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static ISqlLogic NotLogic(this OrLogic or)
    {
        var preview = or.Preview();
        if (preview.IsEmpty)
            return EmptyLogic.Instance;
        if (preview.HasSecond)
            return or.Not();
        return preview.First.Not();
    }
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制Or逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo<TOrLogic>(this OrLogic source, TOrLogic destination)
        where TOrLogic : Logic, IOrLogic
        => destination.AddLogics(source._logics);
    #endregion
    #region MergeTo
    /// <summary>
    /// OrLogic与OrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static TOrLogic MergeTo<TOrLogic>(this OrLogic source, TOrLogic or)
        where TOrLogic : Logic, IOrLogic
    {
        source.CopyTo(or);
        return or;
    }
    /// <summary>
    /// 合并到SqlOrQuery
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static SqlOrQuery MergeTo(this OrLogic source, SqlOrQuery or)
    {
        source.CopyTo(or.Complex);
        return or;
    }
    #endregion
    #region MergeToAnd
    /// <summary>
    /// OrLogic与AndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static Logic MergeToAnd(this OrLogic source, AndLogic and)
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return and;
        if (preview.HasSecond)
            return and.MergeTo(new ComplexAndLogic()).AddOtherCore(source.MergeTo(new ComplexOrLogic()));
        return and.AndCore(preview.First);
    }
    /// <summary>
    /// OrLogic与ComplexAndLogice合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static TComplexAndLogic MergeToAnd<TComplexAndLogic>(this OrLogic source, TComplexAndLogic and)
        where TComplexAndLogic : ComplexLogicBase, IAndLogic
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return and;
        if (preview.HasSecond)
            and.AddOther(source.MergeTo(new ComplexOrLogic()));
        else
            and.AddLogic(preview.First);
        return and;
    }
    #endregion
}
