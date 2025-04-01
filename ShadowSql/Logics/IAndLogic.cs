using ShadowSql.Previews;
using ShadowSql.Queries;
using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// And逻辑
/// </summary>
public interface IAndLogic/* : ISqlLogic*/
{
    //    #region And逻辑
    //    /// <summary>
    //    /// And逻辑
    //    /// </summary>
    //    /// <param name="atomic"></param>
    //    /// <returns></returns>
    //    IAndLogic And(AtomicLogic atomic);
    //    /// <summary>
    //    /// And逻辑
    //    /// </summary>
    //    /// <param name="and"></param>
    //    /// <returns></returns>
    //    IAndLogic And(IAndLogic and);
    //    /// <summary>
    //    /// And逻辑
    //    /// </summary>
    //    /// <param name="or"></param>
    //    /// <returns></returns>
    //    IAndLogic And(IOrLogic or);
    //    #endregion
    //    //#region MergeToAnd
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="and"></param>
    //    ///// <returns></returns>
    //    //IAndLogic MergeTo(AndLogic and);
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="and"></param>
    //    ///// <returns></returns>
    //    //IAndLogic MergeTo(ComplexAndLogic and);
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="query"></param>
    //    ///// <returns></returns>
    //    //SqlAndQuery MergeTo(SqlAndQuery query);
    //    //#endregion
    //    //#region MergeToOr
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="or"></param>
    //    ///// <returns></returns>
    //    //IOrLogic MergeTo(OrLogic or);
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="or"></param>
    //    ///// <returns></returns>
    //    //ComplexOrLogic MergeTo(ComplexOrLogic or);
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="query"></param>
    //    ///// <returns></returns>
    //    //SqlOrQuery MergeTo(SqlOrQuery query);
    //    //#endregion
    //    ///// <summary>
    //    ///// 反转
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //IOrLogic ToOr();
    //    /// <summary>
    //    /// not And为not每个子项的Or
    //    /// </summary>
    //    /// <returns></returns>
    //    new IOrLogic Not();

    //    #region 运算符重载
    //#if NET8_0_OR_GREATER
    //    #region And
    //    /// <summary>
    //    /// 与逻辑
    //    /// </summary>
    //    /// <param name="logic"></param>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public static IAndLogic operator &(IAndLogic logic, IAndLogic other)
    //        => logic.And(other);
    //    #endregion
    //    #region Or
    //    /// <summary>
    //    /// 或逻辑
    //    /// </summary>
    //    /// <param name="logic"></param>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public static IOrLogic operator |(IAndLogic logic, IOrLogic other)
    //        => other.Or(logic);
    //    #endregion
    //    #region Not
    //    /// <summary>
    //    /// 反逻辑
    //    /// </summary>
    //    /// <param name="logic"></param>
    //    /// <returns></returns>
    //    public static IOrLogic operator !(IAndLogic logic)
    //        => logic.Not();
    //    #endregion
    //#endif
    //    #endregion
}

///// <summary>
///// 复合And逻辑
///// </summary>
//public interface IComplexAndLogic : IAndLogic
//{
//    /// <summary>
//    /// 增加内部嵌套复合查询
//    /// </summary>
//    /// <param name="other"></param>
//    void AddOther(IComplexOrLogic other);
//    /// <summary>
//    /// 内部嵌套复合查询
//    /// </summary>
//    IEnumerable<IComplexOrLogic> Others { get; }
//    /// <summary>
//    /// not复合And为复合Or
//    /// </summary>
//    /// <returns></returns>
//    new IComplexOrLogic Not();
//}