using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// 空逻辑扩展方法
/// (与EmptyLogic重载运算符一一对应)
/// </summary>
public static partial class ShadowSqlServices
{
    #region And
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static EmptyLogic And(this EmptyLogic logic, EmptyLogic other)
        => logic;
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AtomicLogic And(this EmptyLogic logic, AtomicLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static AtomicLogic And(this AtomicLogic other, EmptyLogic logic)
        => other;
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this EmptyLogic logic, IAndLogic other)
    //    => other;
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this IAndLogic other, EmptyLogic logic)
    //    => other;
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic And(this EmptyLogic logic, AndLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static AndLogic And(this AndLogic other, EmptyLogic logic)
        => other;
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic And(this EmptyLogic logic, ComplexAndLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static ComplexAndLogic And(this ComplexAndLogic other, EmptyLogic logic)
        => other;
    #endregion
    #region SqlAndQuery
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static SqlAndQuery And(this EmptyLogic logic, SqlAndQuery other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static SqlAndQuery And(this SqlAndQuery other, EmptyLogic logic)
        => other;
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this EmptyLogic logic, IOrLogic other)
    //    => other.ToAnd();
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this IOrLogic other, EmptyLogic logic)
    //  => other.ToAnd();
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this EmptyLogic logic, OrLogic other)
        => other.ToAndCore();
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Logic And(this OrLogic other, EmptyLogic logic)
      => other.ToAndCore();
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this EmptyLogic logic, ComplexOrLogic other)
        => other.ToAndCore();
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Logic And(this ComplexOrLogic other, EmptyLogic logic)
      => other.ToAndCore();
    #endregion
    //#region SqlOrQuery
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static SqlAndQuery And(this EmptyLogic logic, SqlOrQuery other)
    //    => other.ToAnd();
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static SqlAndQuery And(this SqlOrQuery other, EmptyLogic logic)
    //  => other.ToAnd();
    //#endregion
    #endregion
    #region Or
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static EmptyLogic Or(this EmptyLogic logic, EmptyLogic other)
        => logic;
    #region AtomicLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AtomicLogic Or(this EmptyLogic logic, AtomicLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static AtomicLogic Or(this AtomicLogic other, EmptyLogic logic)
        => other;
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this EmptyLogic logic, IAndLogic other)
    //    => other.ToOr();
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IAndLogic other, EmptyLogic logic)
    //    => other.ToOr();
    //#endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this EmptyLogic logic, AndLogic other)
        => other.ToOrCore();
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Logic Or(this AndLogic other, EmptyLogic logic)
        => other.ToOrCore();
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this EmptyLogic logic, ComplexAndLogic other)
        => other.ToOrCore();
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Logic Or(this ComplexAndLogic other, EmptyLogic logic)
        => other.ToOrCore();
    #endregion
    //#region SqlAndQuery
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static SqlOrQuery Or(this EmptyLogic logic, SqlAndQuery other)
    //    => other.ToOr();
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static SqlOrQuery Or(this SqlAndQuery other, EmptyLogic logic)
    //    => other.ToOr();
    //#endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this EmptyLogic logic, IOrLogic other)
    //    => other;
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IOrLogic other, EmptyLogic logic)
    //    => other;
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic Or(this EmptyLogic logic, OrLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static OrLogic Or(this OrLogic other, EmptyLogic logic)
        => other;
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic Or(this EmptyLogic logic, ComplexOrLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static ComplexOrLogic Or(this ComplexOrLogic other, EmptyLogic logic)
        => other;
    #endregion
    //#region SqlOrQuery
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static SqlOrQuery Or(this EmptyLogic logic, SqlOrQuery other)
    //    => other;
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static SqlOrQuery Or(this SqlOrQuery other, EmptyLogic logic)
    //    => other;
    //#endregion
    #endregion
}
