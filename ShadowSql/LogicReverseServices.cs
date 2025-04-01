using ShadowSql.Logics;

namespace ShadowSql;

/// <summary>
/// 逻辑反转扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region ToOr
    #region AndLogic
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    internal static Logic ToOrCore(this AndLogic and)
    {
        return and.LogicCount switch
        {
            0 => new OrLogic(),
            1 => new OrLogic(and.FirstLogic),
            _ => new ComplexOrLogic().AddOtherCore(and.MergeTo(new ComplexAndLogic())),
        };
    }
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    internal static Logic ToOrCore(this AndLogic and, AtomicLogic atomic)
    {
        return and.LogicCount switch
        {
            0 => new OrLogic(atomic),
            1 => new OrLogic(and.FirstLogic).OrCore(atomic),
            _ => new ComplexOrLogic().AddOtherCore(and.MergeTo(new ComplexAndLogic())).OrCore(atomic),
        };
    }
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    internal static Logic ToOrCore(this ComplexAndLogic and)
    {
        var preview = and.Preview();
        if (preview.IsEmpty)
            return new OrLogic();
        if (preview.HasSecond)
            return new ComplexOrLogic().AddOtherCore(and);
        return new OrLogic(preview.First);
    }
    #endregion
    #endregion
    #region ToAnd
    #region OrLogic
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    internal static Logic ToAndCore(this OrLogic or)
    {
        return or.LogicCount switch
        {
            0 => new AndLogic(),
            1 => new AndLogic(or.FirstLogic),
            _ => new ComplexAndLogic().AddOtherCore(or.MergeTo(new ComplexOrLogic())),
        };
    }
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    internal static Logic ToAndCore(this OrLogic or, AtomicLogic atomic)
    {
        return or.LogicCount switch
        {
            0 => new AndLogic(atomic),
            1 => new AndLogic(or.FirstLogic).AndCore(atomic),
            _ => new ComplexAndLogic().AddOtherCore(or.MergeTo(new ComplexOrLogic())).AndCore(atomic),
        };
    }
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    internal static Logic ToAndCore(this ComplexOrLogic or)
    {
        var preview = or.Preview();
        if (preview.IsEmpty)
            return new AndLogic();
        if (preview.HasSecond)
            return new ComplexAndLogic().AddOtherCore(or);
        return new AndLogic(preview.First);
    }
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    internal static Logic ToAndCore(this ComplexOrLogic or, AtomicLogic atomic)
    {
        var preview = or.Preview();
        if (preview.IsEmpty)
            return new AndLogic(atomic);
        if (preview.HasSecond)
            return new ComplexAndLogic().AddOtherCore(or).AndCore(atomic);
        return new AndLogic(preview.First).AndCore(atomic);
    }
    #endregion
    #endregion
}
