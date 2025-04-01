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
        var preview = and.Preview();
        if (preview.IsEmpty)
            return new OrLogic();
        if (preview.HasSecond)
            return new ComplexOrLogic().AddOtherCore(and.MergeTo(new ComplexAndLogic()));
        return new OrLogic(preview.First);
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
        var preview = or.Preview();
        if (preview.IsEmpty)
            return new AndLogic();
        if (preview.HasSecond)
            return new ComplexAndLogic().AddOtherCore(or.MergeTo(new ComplexOrLogic()));
        return new AndLogic(preview.First);
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
    #endregion
    #endregion
}
