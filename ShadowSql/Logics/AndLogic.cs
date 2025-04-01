using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// And逻辑
/// </summary>
public sealed class AndLogic : Logic, IAndLogic, ISqlLogic
{
    /// <summary>
    /// And逻辑
    /// </summary>
    public AndLogic()
    : this([])
    {
    }
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    public AndLogic(AtomicLogic logic)
        : this([logic])
    {
    }
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="items"></param>
    internal AndLogic(List<AtomicLogic> items)
        : base(LogicSeparator.And, items)
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
    /// And查询
    /// </summary>
    /// <returns></returns>
    public override Logic ToAnd()
        => this;
    /// <summary>
    /// 转化为Or查询
    /// </summary>
    /// <returns></returns>
    public override Logic ToOr()
        => this.ToOrCore();
    #endregion
    #region Not
    //IOrLogic IAndLogic.Not()
    //    => this.Not();
    ISqlLogic ISqlLogic.Not()
        => this.NotLogic();
    #endregion

    //#region IAndLogic
    //IAndLogic IAndLogic.And(AtomicLogic atomic)
    //    => this.And(atomic);
    ////IAndLogic IAndLogic.And(IAndLogic and)
    ////    => this.And(and);
    ////IAndLogic IAndLogic.And(IOrLogic or)
    ////    => this.And(or);
    ////IOrLogic IAndLogic.ToOr()
    ////    => this.ToOr();
    //#endregion
    //#region MergeTo
    //internal override IAndLogic MergeTo(AndLogic and)
    //    => this.MergeTo(and);
    //IAndLogic IAndLogic.MergeTo(ComplexAndLogic and)
    //    => this.MergeTo(and);
    //SqlAndQuery IAndLogic.MergeTo(SqlAndQuery query)
    //    => this.MergeTo(query);
    //IOrLogic IAndLogic.MergeTo(OrLogic or)
    //    => this.MergeToOr(or);
    //ComplexOrLogic IAndLogic.MergeTo(ComplexOrLogic or)
    //    => this.MergeToOr(or);
    //SqlOrQuery IAndLogic.MergeTo(SqlOrQuery query)
    //    => this.MergeToOr(query);
    //#endregion
    #region Logic
    //internal override Logic ToAnd()
    //    => this;
    //internal override Logic ToOr()
    //{
    //    var preview = Preview();
    //    if (preview.IsEmpty)
    //        return new OrLogic();
    //    if (preview.HasSecond)
    //        return new ComplexOrLogic().AddOtherCore(this.MergeTo(new ComplexAndLogic()));
    //    return new OrLogic(preview.First);
    //}

    //public AndLogic And(AtomicLogic atomic)
    //{
    //    AddLogic(atomic);
    //    return this;
    //} 
   
    #endregion
    #region 运算符重载
    //AndLogic+类
    #region And
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic operator &(AndLogic logic, AtomicLogic other)
        => logic.AndCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(AndLogic logic, IAndLogic other)
    //    => logic.And(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IAndLogic other, AndLogic logic)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic operator &(AndLogic logic, AndLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(AndLogic logic, ComplexAndLogic other)
        => other.AndCore(logic);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(AndLogic logic, IOrLogic other)
    //    => logic.And(other);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IOrLogic other, AndLogic logic)
    //    => logic.And(other);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(AndLogic logic, OrLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(AndLogic logic, ComplexOrLogic other)
        => logic.AndCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(AndLogic logic, Logic other)
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
    public static Logic operator |(AndLogic logic, AtomicLogic other)
        => logic.OrCore(other);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(AndLogic logic, IOrLogic other)
    //    => other.Or(logic);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IOrLogic other, AndLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(AndLogic logic, OrLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(AndLogic logic, ComplexOrLogic other)
        => logic.OrCore(other);
    #endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(AndLogic logic, AndLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(AndLogic logic, ComplexAndLogic other)
        => other.OrCore(logic);
    #endregion
    #region Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(AndLogic logic, Logic other)
        => logic.Or(other);
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static OrLogic operator !(AndLogic logic)
        => logic.Not();
    #endregion
    #endregion
}