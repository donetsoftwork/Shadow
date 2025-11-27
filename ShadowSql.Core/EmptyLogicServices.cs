using ShadowSql.Logics;

namespace ShadowSql;

/// <summary>
/// 空逻辑扩展方法
/// (与EmptyLogic重载运算符一一对应)
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region And
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static EmptyLogic And(this EmptyLogic logic, EmptyLogic _)
        => logic;
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AtomicLogic And(this EmptyLogic _, AtomicLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static AtomicLogic And(this AtomicLogic other, EmptyLogic _)
        => other;
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="_">空逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this EmptyLogic _, IAndLogic other)
    //    => other;
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="_">空逻辑</param>
    ///// <returns></returns>
    //public static IAndLogic And(this IAndLogic other, EmptyLogic _)
    //    => other;
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic And(this EmptyLogic _, AndLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static AndLogic And(this AndLogic other, EmptyLogic _)
        => other;
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic And(this EmptyLogic logic, IOrLogic other)
    //    => other.ToAnd();
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static IAndLogic And(this IOrLogic other, EmptyLogic logic)
    //  => other.ToAnd();
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this EmptyLogic _, OrLogic other)
        => other.ToAndCore();
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static Logic And(this OrLogic other, EmptyLogic _)
      => other.ToAndCore();
    #endregion
    #endregion
    #region Or
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="_"></param>
    /// <returns></returns>
    public static EmptyLogic Or(this EmptyLogic logic, EmptyLogic _)
        => logic;
    #region AtomicLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AtomicLogic Or(this EmptyLogic _, AtomicLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static AtomicLogic Or(this AtomicLogic other, EmptyLogic _)
        => other;
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this EmptyLogic logic, IAndLogic other)
    //    => other.ToOr();
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IAndLogic other, EmptyLogic logic)
    //    => other.ToOr();
    //#endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this EmptyLogic _, AndLogic other)
        => other.ToOrCore();
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static Logic Or(this AndLogic other, EmptyLogic _)
        => other.ToOrCore();
    #endregion  
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic Or(this EmptyLogic logic, IOrLogic other)
    //    => other;
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static IOrLogic Or(this IOrLogic other, EmptyLogic logic)
    //    => other;
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic Or(this EmptyLogic _, OrLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static OrLogic Or(this OrLogic other, EmptyLogic _)
        => other;
    #endregion
    #endregion
}
