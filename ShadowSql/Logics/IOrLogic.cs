using ShadowSql.Previews;
using ShadowSql.Queries;
using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// Or逻辑
/// </summary>
public interface IOrLogic/* : ISqlLogic*/
{
    //    #region Or逻辑
    //    /// <summary>
    //    /// Or逻辑
    //    /// </summary>
    //    /// <param name="atomic"></param>
    //    /// <returns></returns>
    //    IOrLogic Or(AtomicLogic atomic);
    //    /// <summary>
    //    /// Or逻辑
    //    /// </summary>
    //    /// <param name="or"></param>
    //    /// <returns></returns>
    //    IOrLogic Or(IOrLogic or);
    //    /// <summary>
    //    /// Or逻辑
    //    /// </summary>
    //    /// <param name="and"></param>
    //    /// <returns></returns>
    //    IOrLogic Or(IAndLogic and);
    //    #endregion
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
    //    //IOrLogic MergeTo(ComplexOrLogic or);
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="query"></param>
    //    ///// <returns></returns>
    //    //SqlOrQuery MergeTo(SqlOrQuery query);
    //    //#endregion
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
    //    //ComplexAndLogic MergeTo(ComplexAndLogic and);
    //    ///// <summary>
    //    ///// 合并
    //    ///// </summary>
    //    ///// <param name="query"></param>
    //    ///// <returns></returns>
    //    //SqlAndQuery MergeTo(SqlAndQuery query);
    //    //#endregion
    //    ///// <summary>
    //    ///// 子逻辑预览
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //IPreview<AtomicLogic> Preview();
    //    /// <summary>
    //    /// 反转
    //    /// </summary>
    //    /// <returns></returns>
    //    IAndLogic ToAnd();
    //    /// <summary>
    //    /// not Or为not每个子项的And
    //    /// </summary>
    //    /// <returns></returns>
    //    new IAndLogic Not();

    //    #region 运算符重载
    //#if NET8_0_OR_GREATER
    //    #region And 
    //    /// <summary>
    //    /// 与逻辑
    //    /// </summary>
    //    /// <param name="logic"></param>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public static IAndLogic operator &(IOrLogic logic, IAndLogic other)
    //        => other.And(logic);
    //    #endregion
    //    #region Or
    //    /// <summary>
    //    /// 或逻辑
    //    /// </summary>
    //    /// <param name="logic"></param>
    //    /// <param name="other"></param>
    //    /// <returns></returns>
    //    public static IOrLogic operator |(IOrLogic logic, IOrLogic other)
    //        => logic.Or(other);
    //    #endregion
    //    #region Not
    //    /// <summary>
    //    /// 反逻辑
    //    /// </summary>
    //    /// <param name="logic"></param>
    //    /// <returns></returns>
    //    public static IAndLogic operator !(IOrLogic logic)
    //        => logic.Not();
    //    #endregion
    //#endif
    //    #endregion
}

///// <summary>
///// 复合Or逻辑
///// </summary>
//public interface IComplexOrLogic : IOrLogic
//{
//    /// <summary>
//    /// 增加内部嵌套复合查询
//    /// </summary>
//    /// <param name="other"></param>
//    void AddOther(IComplexAndLogic other);
//    /// <summary>
//    /// 内部嵌套复合查询
//    /// </summary>
//    IEnumerable<IComplexAndLogic> Others { get; }
//    /// <summary>
//    /// not复合Or为复合And
//    /// </summary>
//    /// <returns></returns>
//    new IComplexAndLogic Not();
//}
