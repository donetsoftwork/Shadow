using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// Or逻辑
/// </summary>
public sealed class OrLogic : Logic, IOrLogic, ISqlLogic
{
    /// <summary>
    /// Or逻辑
    /// </summary>
    public OrLogic()
    : this([])
    {
    }
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="logic"></param>
    public OrLogic(AtomicLogic logic)
        : this([logic])
    {
    }
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="items"></param>
    internal OrLogic(List<AtomicLogic> items)
        : base(LogicSeparator.Or, items)
    {
    }
    #region Logic
    #region And逻辑
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public override Logic And(AtomicLogic atomic)
        => this.AndCore(atomic);
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public override Logic And(AndLogic and)
        => this.AndCore(and);
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public override Logic And(ComplexAndLogic and)
        => this.AndCore(and);
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public override Logic And(OrLogic or)
        => this.AndCore(or);
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public override Logic And(ComplexOrLogic or)
        => this.AndCore(or);
    ///// <summary>
    ///// And逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public override Logic And(Logic logic);
    #endregion
    #region Or逻辑
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public override Logic Or(AtomicLogic atomic)
        => this.OrCore(atomic);
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public override Logic Or(OrLogic or)
        => this.OrCore(or);
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public override Logic Or(ComplexOrLogic or)
        => this.OrCore(or);
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public override Logic Or(AndLogic and)
        => this.OrCore(and);
    /// <summary>
    /// Or逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public override Logic Or(ComplexAndLogic and)
        => this.OrCore(and);
    ///// <summary>
    ///// Or逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public override Logic Or(Logic logic);
    #endregion
    /// <summary>
    /// 转化为And查询
    /// </summary>
    /// <returns></returns>
    public override Logic ToAnd()
        => this.ToAndCore();
    /// <summary>
    /// Or查询
    /// </summary>
    /// <returns></returns>
    public override Logic ToOr()
        => this;
    #endregion
    #region Not
    //IAndLogic IOrLogic.Not()
    //    => this.Not();
    ISqlLogic ISqlLogic.Not()
        => this.NotLogic();
    #endregion
    //#region IOrLogic
    //IOrLogic IOrLogic.Or(AtomicLogic atomic)
    //    => this.Or(atomic);
    //IOrLogic IOrLogic.Or(IOrLogic or)
    //    => this.Or(or);
    //IOrLogic IOrLogic.Or(IAndLogic and)
    //    => this.Or(and);
    //IAndLogic IOrLogic.ToAnd()
    //    => this.ToAnd();
    //#endregion
    //#region MergeTo
    //IOrLogic IOrLogic.MergeTo(OrLogic or)
    //    => this.MergeTo(or);
    //IOrLogic IOrLogic.MergeTo(ComplexOrLogic or)
    //    => this.MergeTo(or);
    //SqlOrQuery IOrLogic.MergeTo(SqlOrQuery query)
    //    => this.MergeTo(query);
    //IAndLogic IOrLogic.MergeTo(AndLogic and)
    //    => this.MergeToAnd(and);
    //ComplexAndLogic IOrLogic.MergeTo(ComplexAndLogic and)
    //    => this.MergeToAnd(and);
    //SqlAndQuery IOrLogic.MergeTo(SqlAndQuery query)
    //    => this.MergeToAnd(query);
    //#endregion
    #region 运算符重载
    #region And
    #region AtomicLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, AtomicLogic other)
        => logic.AndCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(OrLogic logic, IAndLogic other)
    //    => other.And(logic);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IAndLogic other, OrLogic logic)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, AndLogic other)
        => other.AndCore(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IAndLogic operator &(OrLogic logic, ComplexAndLogic other)
        => other.AndCore(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(OrLogic logic, IOrLogic other)
    //    => logic.And(other);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IOrLogic other, OrLogic logic)
    //    => other.And(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, OrLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, ComplexOrLogic other)
        => logic.AndCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, Logic other)
        => logic.And(other);
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
    public static OrLogic operator |(OrLogic logic, AtomicLogic other)
        => logic.OrCore(other);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(OrLogic logic, IOrLogic other)
    //    => logic.Or(other);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IOrLogic other, OrLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic operator |(OrLogic logic, OrLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(OrLogic logic, ComplexOrLogic other)
        => logic.OrCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(OrLogic logic, IAndLogic other)
    //    => logic.Or(other);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IAndLogic other, OrLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(OrLogic logic, AndLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(OrLogic logic, ComplexAndLogic other)
        => logic.OrCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(OrLogic logic, Logic other)
        => logic.Or(other);
    #endregion
    #endregion
    #endregion
}
