using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// 或逻辑扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
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
    /// 与逻辑
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
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this OrLogic source, OrLogic other)
    {
        return other.LogicCount switch
        {
            0 => source.ToAndCore(),
            1 => source.ToAndCore(other.FirstLogic),
            _ => source.LogicCount switch
            {
                0 => other.ToAndCore(),
                1 => other.ToAndCore(source.FirstLogic),
                _ => other.MergeToAnd(new ComplexAndLogic().AddOtherCore(source.MergeTo(new ComplexOrLogic()))),
            },
        };
    }
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic AndCore(this OrLogic source, ComplexOrLogic other)
    {
        return source.LogicCount switch
        {
            0 => other.ToAndCore(),
            1 => other.ToAndCore(source.FirstLogic),
            _ => other.MergeToAnd(new ComplexAndLogic().AddOtherCore(source.MergeTo(new ComplexOrLogic()))),
        };
    }
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// not Or为not每个子项的And
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static AndLogic Not(this OrLogic source)
    {
        var and = new AndLogic();
        and.NotLogics(source._logics);
        return and;
    }
    /// <summary>
    /// Not逻辑简化
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    internal static ISqlLogic NotLogic(this OrLogic source)
    {
        return source.LogicCount switch
        {
            0 => EmptyLogic.Instance,
            1 => source.FirstLogic.Not(),
            _ => source.Not(),
        };
    }
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制或逻辑
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
        return source.LogicCount switch
        {
            0 => and,
            1 => and.AndCore(source.FirstLogic),
            _ => and.MergeTo(new ComplexAndLogic()).AddOtherCore(source.MergeTo(new ComplexOrLogic())),
        };
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
        return source.LogicCount switch
        {
            0 => and,
            1 => and.AndCore(source.FirstLogic),
            _ => and.AddOtherCore(source.MergeTo(new ComplexOrLogic())),
        };
    }
    #endregion
}
