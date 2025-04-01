using ShadowSql.Logics;

namespace ShadowSql;

/// <summary>
/// 原子逻辑扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    //AtomicLogic+类
    //AtomicLogic+接口
    //接口+AtomicLogic
    #region And
    #region AtomicLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic And(this AtomicLogic logic, AtomicLogic other)
        => new AndLogic(logic)
            .AndCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// And逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this AtomicLogic logic, IAndLogic other)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic And(this AtomicLogic logic, AndLogic other)
        => other.AndCore(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic And(this AtomicLogic logic, ComplexAndLogic other)
        => other.AndCore(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// And逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this AtomicLogic logic, IOrLogic other)
    //    => other.And(logic);
    ///// <summary>
    ///// And逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this IOrLogic other, AtomicLogic logic)
    //    => other.ToAnd()
    //        .And(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this AtomicLogic logic, OrLogic other)
        => other.AndCore(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this AtomicLogic logic, ComplexOrLogic other)
        => other.AndCore(logic);
    #endregion
    #endregion
    #region Or
    #region AtomicLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic Or(this AtomicLogic logic, AtomicLogic other)
        => new OrLogic(logic)
            .OrCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this AtomicLogic logic, IAndLogic other)
    //    => other.Or(logic);
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IAndLogic other, AtomicLogic logic)
    //  => other.ToOr()
    //        .Or(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this AtomicLogic logic, AndLogic other)
        => other.OrCore(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this AtomicLogic logic, ComplexAndLogic other)
        => other.OrCore(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this AtomicLogic logic, IOrLogic other)
    //    => other.Or(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic Or(this AtomicLogic logic, OrLogic other)
        => other.OrCore(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic Or(this AtomicLogic logic, ComplexOrLogic other)
        => other.OrCore(logic);
    #endregion
    #endregion
}
