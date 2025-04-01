using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 原子逻辑基类
/// </summary>
public abstract class AtomicLogic : ISqlLogic
{
    #region AtomicLogic
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public abstract bool TryWrite(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    public abstract AtomicLogic Not();
    ISqlLogic ISqlLogic.Not()
        => Not();
    #endregion
    #region 运算符重载
    //AtomicLogic+类
    //AtomicLogic+接口
    //接口+AtomicLogic
    #region And
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic operator &(AtomicLogic logic, AtomicLogic other)
        => logic.And(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(AtomicLogic logic, IAndLogic other)
    //    => other.And(logic);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IAndLogic other, AtomicLogic logic)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic operator &(AtomicLogic logic, AndLogic other)
        => other.AndCore(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(AtomicLogic logic, ComplexAndLogic other)
        => other.AndCore(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(AtomicLogic logic, IOrLogic other)
    //    => other.And(logic);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IOrLogic other, AtomicLogic logic)
    //    => other.And(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(AtomicLogic logic, OrLogic other)
        => other.AndCore(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(AtomicLogic logic, ComplexOrLogic other)
        => other.AndCore(logic);
    #endregion
    #endregion
    #region Or
    #region AtomicLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic operator |(AtomicLogic logic, AtomicLogic other)
        => logic.Or(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(AtomicLogic logic, IAndLogic other)
    //    => other.Or(logic);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IAndLogic other, AtomicLogic logic)
    //  => other.Or(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(AtomicLogic logic, AndLogic other)
        => other.OrCore(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(AtomicLogic logic, ComplexAndLogic other)
        => other.OrCore(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(AtomicLogic logic, IOrLogic other)
    //    => other.Or(logic);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IOrLogic other, AtomicLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic operator |(AtomicLogic logic, OrLogic other)
        => other.OrCore(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(AtomicLogic logic, ComplexOrLogic other)
        => other.OrCore(logic);
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static AtomicLogic operator !(AtomicLogic logic)
        => logic.Not();
    #endregion
    #endregion
}

