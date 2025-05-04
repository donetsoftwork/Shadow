using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// 复合或逻辑扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region Or
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this ComplexOrLogic logic, AndLogic other)
        => other.MergeToOr(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this ComplexOrLogic logic, ComplexAndLogic other)
        => other.MergeToOr(logic);
    #endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this ComplexOrLogic logic, OrLogic other)
        => other.MergeTo(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this ComplexOrLogic logic, ComplexOrLogic other)
        => other.MergeTo(logic);
    #endregion
    #endregion
    #region And
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this ComplexOrLogic or, AtomicLogic other)
    {
        var logic = or.ToAndCore();
        logic.AddLogic(other);
        return logic;
    }
    #endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this ComplexOrLogic logic, AndLogic other)
    {
        var preview = logic.Preview();
        if (preview.IsEmpty)
            return other;
        if (preview.HasSecond)
            return other.MergeTo(new ComplexAndLogic().AddOtherCore(logic));
        return other.MergeTo(new AndLogic(preview.First));
    }
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this ComplexOrLogic logic, ComplexAndLogic other)
        => logic.MergeToAnd(other);
    #endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this ComplexOrLogic logic, OrLogic other)
        => other.MergeToAnd(logic.MergeToAnd(new ComplexAndLogic()));
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexAndLogic AndCore(this ComplexOrLogic logic, ComplexOrLogic other)
        => other.MergeToAnd(logic.MergeToAnd(new ComplexAndLogic()));
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// not And为not每个子项的Or
    /// </summary>
    /// <returns></returns>
    public static ComplexAndLogic Not(this ComplexOrLogic or)
    {
        var and = new ComplexAndLogic();
        and.NotLogics(or._logics);
        foreach (var other in or._others)
        {
            if (other is ComplexAndLogic complex)
                and.AddOtherCore(complex.Not());
        }
        and.NotOthers(or);
        return and;
    }
    /// <summary>
    /// Not逻辑简化
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static ISqlLogic NotLogic(this ComplexOrLogic or)
    {
        var preview = or.Preview();
        if (preview.IsEmpty)
            return EmptyLogic.Instance;
        if (preview.HasSecond)
            return or.Not();
        return preview.First.Not();
    }
    #endregion
    #region Others    
    /// <summary>
    /// 添加包装后的And查询
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic AddOtherCore(this ComplexOrLogic logic, ComplexAndLogic other)        
    {
        logic.AddOther(other);
        return logic;
    }
    /// <summary>
    /// not子项
    /// </summary>
    /// <param name="and"></param>
    /// <param name="or"></param>
    internal static void NotOthers(this ComplexOrLogic or, ComplexAndLogic and)
    {
        foreach (var other in and._others)
        {
            if (other is ComplexOrLogic complex)
                or.AddOtherCore(complex.Not());
        }
    }
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制复合或逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo(this ComplexOrLogic source, ComplexOrLogic destination)
    {
        destination.AddLogics(source._logics);
        foreach (var other in source._others)
            destination.AddOther(other);
    }
    #endregion
    #region MergeTo
    /// <summary>
    /// ComplexOrLogic与ComplexOrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static ComplexOrLogic MergeTo(this ComplexOrLogic source, ComplexOrLogic or)
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
    internal static SqlOrQuery MergeTo(this ComplexOrLogic source, SqlOrQuery or)
    {
        source.MergeTo(or.Complex);
        return or;
    }
    /// <summary>
    /// ComplexOrLogic与OrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static ComplexOrLogic MergeTo(this ComplexOrLogic source, OrLogic or)
    {
        or.CopyTo(source);
        return source;
    }
    #endregion
    #region MergeToAnd
    /// <summary>
    /// ComplexOrLogic与ComplexAndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static ComplexAndLogic MergeToAnd(this ComplexOrLogic source, ComplexAndLogic and)
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return and;
        if (preview.HasSecond)
            and.AddOther(source);
        else
            and.AddLogic(preview.First);
        return and;
    }
    /// <summary>
    /// ComplexOrLogic与AndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static Logic MergeToAnd(this ComplexOrLogic source, AndLogic and)
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return and;
        if (preview.HasSecond)
            return and.MergeTo(new ComplexAndLogic()).AddOtherCore(source);
        return and.AndCore(preview.First);
    }
    #endregion
}
