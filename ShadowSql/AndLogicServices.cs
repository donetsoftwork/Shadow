using ShadowSql.Logics;
using ShadowSql.Queries;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// 与逻辑扩展方法
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
    /// 或逻辑
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
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this AndLogic source, AndLogic other)
    {
        return other.LogicCount switch
        {
            0 => source.ToOrCore(),
            1 => source.MergeToOr(new OrLogic(other.FirstLogic)),
            _ => source.LogicCount switch
            {
                0 => other.ToOrCore(),
                1 => other.ToOrCore(source.FirstLogic),
                _ => other.MergeToOr(new ComplexOrLogic().AddOtherCore(source.MergeTo(new ComplexAndLogic()))),
            },
        };
    }
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static ComplexOrLogic OrCore(this AndLogic logic, ComplexAndLogic other)
        => other.MergeToOr(logic.MergeToOr(new ComplexOrLogic()));
    #endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic OrCore(this AndLogic logic, OrLogic other)
        => logic.MergeToOr(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
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
        return and.LogicCount switch
        {
            0 => EmptyLogic.Instance,
            1 => and.FirstLogic.Not(),
            _ => and.Not(),
        };
    }
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制与逻辑
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
        switch(source.LogicCount)
        {
            case 0:
                return or;
            case 1:
                return or.OrCore(source.FirstLogic);
            default:
                switch (or.LogicCount)
                {
                    case 0:
                        return source.ToOr();
                    default:
                        var logic = source.ToOr();
                        logic.AddLogics(or._logics);
                        return logic;
                }
        }
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
