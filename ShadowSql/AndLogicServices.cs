using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// And逻辑扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    //AndLogic+类
    #region And
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static TAndLogic AndCore<TAndLogic>(this TAndLogic logic, AtomicLogic other)
        where TAndLogic : Logic, IAndLogic
    {
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
    //public static IAndLogic And(this AndLogic logic, IAndLogic other)
    //    => other.MergeTo(logic);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this IAndLogic other, AndLogic logic)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static AndLogic AndCore(this AndLogic logic, AndLogic other)
        => other.MergeTo(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static TComplexAndLogic AndCore<TComplexAndLogic>(this AndLogic logic, TComplexAndLogic other)
        where TComplexAndLogic : ComplexLogicBase, IAndLogic
        => logic.MergeTo(other);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this AndLogic logic, IOrLogic other)
    //    => other.MergeTo(logic);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this IOrLogic other, AndLogic logic)
    //    => other.MergeTo(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this AndLogic logic, OrLogic other)
        => other.MergeToAnd(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this AndLogic logic, ComplexOrLogic other)
        => other.MergeToAnd(logic);
    #endregion
    #endregion
    #region Or
    #region AtomicLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this AndLogic and, AtomicLogic other)
    {
        var logic = and.ToOrCore();
        logic.AddLogic(other);
        return logic;
    }
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this AndLogic logic, IAndLogic other)
    //    => logic.ToOr()
    //        .Or(other);
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IAndLogic other, AndLogic logic)
    //    => other.ToOr()
    //        .Or(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this AndLogic and, AndLogic other)
    {
        var preview = and.Preview();
        if (preview.IsEmpty)
            return other.ToOrCore();
        if (preview.HasSecond)
            return other.MergeToOr(new ComplexOrLogic().AddOtherCore(and.MergeTo(new ComplexAndLogic())));
        return other.MergeToOr(new OrLogic(preview.First));
    }
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this AndLogic logic, ComplexAndLogic other)
        => other.MergeToOr(logic.MergeToOr(new ComplexOrLogic()));
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this AndLogic logic, IOrLogic other)
    //    => logic.ToOr()
    //        .Or(other);
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IOrLogic other, AndLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this AndLogic logic, OrLogic other)
        => logic.MergeToOr(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this AndLogic logic, ComplexOrLogic other)
        => logic.MergeToOr(other);
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// not And为not每个子项的Or
    /// </summary>
    /// <returns></returns>
    public static OrLogic Not(this AndLogic and)
    {
        var or = new OrLogic();
        or.NotLogics(and._logics);
        return or;
    }
    /// <summary>
    /// Not逻辑简化
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static ISqlLogic NotLogic(this AndLogic and)
    {
        var preview = and.Preview();
        if (preview.IsEmpty)
            return EmptyLogic.Instance;
        if (preview.HasSecond)
            return and.Not();
        return preview.First.Not();
    }
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制And逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo<TAndLogic>(this AndLogic source, TAndLogic destination)
        where TAndLogic : Logic, IAndLogic
        => destination.AddLogics(source._logics);
    #endregion
    #region MergeTo
    /// <summary>
    /// 合并到AndLogic
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static TAndLogic MergeTo<TAndLogic>(this AndLogic source, TAndLogic and)
        where TAndLogic : Logic, IAndLogic
    {
        source.CopyTo(and);
        return and;
    }
    /// <summary>
    /// 合并到SqlAndQuery
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static SqlAndQuery MergeTo(this AndLogic source, SqlAndQuery and)
    {
        source.CopyTo(and.Complex);
        return and;
    }
    #endregion
    #region MergeToOr
    /// <summary>
    /// 合并到OrLogic
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static Logic MergeToOr(this AndLogic source, OrLogic or)
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return or;
        if (preview.HasSecond)
            return or.MergeTo(new ComplexOrLogic()).AddOtherCore(source.MergeTo(new ComplexAndLogic()));
        return or.OrCore(preview.First);
    }
    /// <summary>
    /// 合并到ComplexOrLogic
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static TComplexOrLogic MergeToOr<TComplexOrLogic>(this AndLogic source, TComplexOrLogic or)
        where TComplexOrLogic : ComplexLogicBase, IOrLogic
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return or;
        if (preview.HasSecond)
            or.AddOther(source.MergeTo(new ComplexAndLogic()));
        else
            or.AddLogic(preview.First);
        return or;
    }
    #endregion
}
