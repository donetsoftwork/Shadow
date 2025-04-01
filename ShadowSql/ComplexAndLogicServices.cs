using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// 复合与逻辑扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region And
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this ComplexAndLogic logic, AndLogic other)
        => other.MergeTo(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this ComplexAndLogic logic, ComplexAndLogic other)
        => other.MergeTo(logic);
    #endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this ComplexAndLogic logic, OrLogic other)
        => other.MergeToAnd(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this ComplexAndLogic logic, ComplexOrLogic other)
        => other.MergeToAnd(logic);
    #endregion
    #endregion
    #region Or
    #region AtomicLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this ComplexAndLogic and, AtomicLogic other)
    {
        var logic = and.ToOrCore();
        logic.AddLogic(other);
        return logic;
    }
    #endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this ComplexAndLogic logic, OrLogic other)
    {
        var preview = logic.Preview();
        if (preview.IsEmpty)
            return other;
        if (preview.HasSecond)
            return other.MergeTo(new ComplexOrLogic().AddOtherCore(logic));
        return other.MergeTo(new OrLogic(preview.First));
    }
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this ComplexAndLogic logic, ComplexOrLogic other)
        => logic.MergeToOr(other);
    #endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this ComplexAndLogic logic, AndLogic other)
        => other.MergeToOr(logic.MergeToOr(new ComplexOrLogic()));
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this ComplexAndLogic logic, ComplexAndLogic other)
        => other.MergeToOr(logic.MergeToOr(new ComplexOrLogic()));
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// not And为not每个子项的Or
    /// </summary>
    /// <returns></returns>
    public static ComplexOrLogic Not(this ComplexAndLogic and)
    {
        var or = new ComplexOrLogic();
        or.NotLogics(and._logics);
        or.NotOthers(and);
        return or;
    }
    /// <summary>
    /// Not逻辑简化
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static ISqlLogic NotLogic(this ComplexAndLogic and)
    {
        var preview = and.Preview();
        if (preview.IsEmpty)
            return EmptyLogic.Instance;
        if (preview.HasSecond)
            return and.Not();
        return preview.First.Not();
    }
    #endregion
    #region Others
    /// <summary>
    /// 添加包装后的Or查询
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static TComplexAndLogic AddOtherCore<TComplexAndLogic, TComplexOrLogic>(this TComplexAndLogic logic, TComplexOrLogic other)
        where TComplexAndLogic : ComplexLogicBase, IAndLogic
        where TComplexOrLogic : ComplexLogicBase, IOrLogic
    {
        logic.AddOther(other);
        return logic;
    }
    /// <summary>
    /// not子项
    /// </summary>
    /// <param name="and"></param>
    /// <param name="or"></param>
    internal static void NotOthers<TComplexAndLogic, TComplexOrLogic>(this TComplexAndLogic and, TComplexOrLogic or)
        where TComplexAndLogic : ComplexLogicBase, IAndLogic
        where TComplexOrLogic : ComplexLogicBase, IOrLogic
    {
        foreach (var other in or._others)
        {
            if (other is ComplexAndLogic complex)
                and.AddOtherCore(complex.Not());
        }
    }
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制复合与逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo<TComplexAndLogic>(this ComplexAndLogic source, TComplexAndLogic destination)
        where TComplexAndLogic : ComplexLogicBase, IAndLogic
    {
        destination.AddLogics(source._logics);
        foreach (var other in source._others)
            destination.AddOther(other);
    }
    #endregion
    #region MergeTo
    /// <summary>
    /// ComplexAndLogic与ComplexAndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static TComplexAndLogic MergeTo<TComplexAndLogic>(this ComplexAndLogic source, TComplexAndLogic and)
        where TComplexAndLogic : ComplexLogicBase, IAndLogic
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
    internal static SqlAndQuery MergeTo(this ComplexAndLogic source, SqlAndQuery and)
    {
        source.CopyTo(and.Complex);
        return and;
    }
    
    /// <summary>
    /// ComplexAndLogic与AndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static ComplexAndLogic MergeTo(this ComplexAndLogic source, AndLogic and)
    {
        and.CopyTo(source);
        return source;
    }
    #endregion
    #region MergeToOr
    /// <summary>
    /// ComplexAndLogic与ComplexOrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static TComplexOrLogic MergeToOr<TComplexOrLogic>(this ComplexAndLogic source, TComplexOrLogic or)
        where TComplexOrLogic : ComplexLogicBase, IOrLogic
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return or;
        if (preview.HasSecond)
            or.AddOther(source);
        else
            or.AddLogic(preview.First);
        return or;
    }
    /// <summary>
    /// ComplexAndLogic与OrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static Logic MergeToOr(this ComplexAndLogic source, OrLogic or)
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return or;
        if (preview.HasSecond)
            return or.MergeTo(new ComplexOrLogic()).AddOtherCore(source);
        return or.OrCore(preview.First);
    }
    #endregion
}
