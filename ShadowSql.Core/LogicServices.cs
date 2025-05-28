using ShadowSql.Logics;

namespace ShadowSql;

/// <summary>
/// 逻辑扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    //Logic只有4个实现类,每个实现类一个if分支
    //如果增加实现类需要增加if
    #region And
    #region Logic+Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this Logic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.And(and);
        else if (other is OrLogic or)
            return logic.And(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.And(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.And(complexOr);
        return logic;
    }
    #endregion
    #region AtomicLogic+Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this AtomicLogic logic, Logic other)
        => other.And(logic);
    #endregion
    #region AndLogic+Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this AndLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.AndCore(and);
        else if (other is OrLogic or)
            return logic.AndCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.AndCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.AndCore(complexOr);
        return logic;
    }
    #endregion
    #region ComplexAndLogic+Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this ComplexAndLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.AndCore(and);
        else if (other is OrLogic or)
            return logic.AndCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.AndCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.AndCore(complexOr);
        return logic;
    }
    #endregion
    #region OrLogic+Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this OrLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.AndCore(and);
        else if (other is OrLogic or)
            return logic.AndCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.AndCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.AndCore(complexOr);
        return logic;
    }
    #endregion
    #region ComplexOrLogic+Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this ComplexOrLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.AndCore(and);
        else if (other is OrLogic or)
            return logic.AndCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.AndCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.AndCore(complexOr);
        return logic;
    }
    #endregion
    #endregion
    #region Or
    #region Logic+Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this Logic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.Or(and);
        else if (other is OrLogic or)
            return logic.Or(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.Or(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.Or(complexOr);
        return logic;
    }
    #endregion
    #region AtomicLogic+Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this AtomicLogic logic, Logic other)
        => other.Or(logic);
    #endregion
    #region OrLogic+Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this OrLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.OrCore(and);
        else if (other is OrLogic or)
            return logic.OrCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.OrCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.OrCore(complexOr);
        return logic;
    }
    #endregion
    #region ComplexOrLogic+Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this ComplexOrLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.OrCore(and);
        else if (other is OrLogic or)
            return logic.OrCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.OrCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.OrCore(complexOr);
        return logic;
    }
    #endregion
    #region AndLogic+Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this AndLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.OrCore(and);
        else if (other is OrLogic or)
            return logic.OrCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.OrCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.OrCore(complexOr);
        return logic;
    }
    #endregion
    #region ComplexAndLogic+Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this ComplexAndLogic logic, Logic other)
    {
        if (other is AndLogic and)
            return logic.OrCore(and);
        else if (other is OrLogic or)
            return logic.OrCore(or);
        else if (other is ComplexAndLogic complexAnd)
            return logic.OrCore(complexAnd);
        else if (other is ComplexOrLogic complexOr)
            return logic.OrCore(complexOr);
        return logic;
    }
    #endregion
    #endregion
    #region MergeTo
    /// <summary>
    /// MergeTo
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    internal static Logic MergeTo(this Logic logic, Logic other)
    {
        if (other is AndLogic and)
            return and.And(logic);
        else if (other is OrLogic or)
            return or.Or(logic);
        else if (other is ComplexAndLogic complexAnd)
            return complexAnd.And(logic);
        else if (other is ComplexOrLogic complexOr)
            return complexOr.Or(logic);
        return logic;
    }
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    public static Logic Not(this Logic logic)
    {
        if (logic is AndLogic and)
            return and.Not();
        else if(logic is OrLogic or)
            return or.Not();
        else if(logic is ComplexAndLogic complexAnd)
            return complexAnd.Not();
        else if (logic is ComplexOrLogic complexOr)
            return complexOr.Not();
        return logic;
    }    
    #endregion
}
