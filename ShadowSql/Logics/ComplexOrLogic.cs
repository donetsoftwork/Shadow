using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// 复合Or逻辑
/// </summary>
public class ComplexOrLogic : ComplexLogicBase, IOrLogic, ISqlLogic
{
    /// <summary>
    /// 复合Or逻辑
    /// </summary>
    public ComplexOrLogic()
        : this([], [])
    {
    }
    /// <summary>
    /// 复合Or逻辑
    /// </summary>
    /// <param name="items"></param>
    /// <param name="others"></param>
    internal ComplexOrLogic(List<AtomicLogic> items, List<ComplexLogicBase> others)
        : base(LogicSeparator.Or, items, others)
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
    ISqlLogic ISqlLogic.Not()
        => this.NotLogic();
    #endregion
    //#region IOrLogic
    //IOrLogic IOrLogic.Or(AtomicLogic atomic)
    //    => this.Or(atomic);
    //IOrLogic IOrLogic.Or(IOrLogic or)
    //=> this.Or(or);
    //IOrLogic IOrLogic.Or(IAndLogic and)
    //    => this.Or(and);
    //IAndLogic IOrLogic.ToAnd()
    //    => this.ToAnd();
    //IAndLogic IOrLogic.Not()
    //    => this.Not();
    //#endregion
    //#region IComplexOrLogic
    //void IComplexOrLogic.AddOther(IComplexAndLogic other)
    //    => _others.Add(other);
    //IEnumerable<IComplexAndLogic> IComplexOrLogic.Others
    //    => _others;
    //IComplexAndLogic IComplexOrLogic.Not()
    //    => this.Not();
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
    //#region Logic
    //internal override Logic ToAnd()
    //{
    //    var preview = Preview();
    //    if (preview.IsEmpty)
    //        return new AndLogic();
    //    if (preview.HasSecond)
    //        return new ComplexAndLogic().AddOtherCore(this);
    //    return new AndLogic(preview.First);
    //}
    //internal override Logic ToOr()
    //    => this;
    //#endregion
    #region 运算符重载
    #region Or
    #region AtomicLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexOrLogic logic, AtomicLogic other)
        => logic.OrCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static ComplexOrLogic operator |(ComplexOrLogic logic, IAndLogic other)
    //    => logic.Or(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static ComplexOrLogic operator |(IAndLogic other, ComplexOrLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexOrLogic logic, AndLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexOrLogic logic, ComplexAndLogic other)
        => logic.OrCore(other);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(ComplexOrLogic logic, IOrLogic other)
    //    => logic.Or(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IOrLogic other, ComplexOrLogic logic)
    //    => logic.Or(other);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexOrLogic logic, OrLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexOrLogic logic, ComplexOrLogic other)
        => logic.OrCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(ComplexOrLogic logic, Logic other)
        => logic.And(other);
    #endregion
    #endregion
    #region And
    #region AtomicLogic
    /// <summary>
    /// And逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(ComplexOrLogic logic, AtomicLogic other)
        => logic.AndCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(ComplexOrLogic logic, IAndLogic other)
    //    => logic.And(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IAndLogic other, ComplexOrLogic logic)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(ComplexOrLogic logic, AndLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexOrLogic logic, ComplexAndLogic other)
        => logic.AndCore(other);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(ComplexOrLogic logic, IOrLogic other)
    //    => logic.And(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IOrLogic other, ComplexOrLogic logic)
    //    => logic.And(other);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexOrLogic logic, OrLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexOrLogic logic, ComplexOrLogic other)
        => logic.AndCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(ComplexOrLogic logic, Logic other)
        => logic.Or(other);
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator !(ComplexOrLogic logic)
        => logic.Not();
    #endregion
    #endregion
}
