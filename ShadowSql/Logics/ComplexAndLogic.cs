using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// 复合And逻辑
/// </summary>
public class ComplexAndLogic : ComplexLogicBase, IAndLogic, ISqlLogic
{
    /// <summary>
    /// 复合And逻辑
    /// </summary>
    public ComplexAndLogic()
        : this([], [])
    {
    }
    /// <summary>
    /// 复合And逻辑
    /// </summary>
    /// <param name="items"></param>
    /// <param name="others"></param>
    internal ComplexAndLogic(List<AtomicLogic> items, List<ComplexLogicBase> others)
        : base(LogicSeparator.And, items, others)
    {
    }
    #region 配置
    #endregion
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
    ISqlLogic ISqlLogic.Not()
        => this.NotLogic();
    #endregion
    //#region IAndLogic
    //IAndLogic IAndLogic.And(AtomicLogic atomic)
    //    => this.And(atomic);
    //IAndLogic IAndLogic.And(IAndLogic and)
    //    => this.And(and);
    //IAndLogic IAndLogic.And(IOrLogic or)
    //    => this.And(or);
    //IOrLogic IAndLogic.ToOr()
    //    => this.ToOr();
    //IOrLogic IAndLogic.Not()
    //    => this.Not();
    //#endregion
    //#region IComplexAndLogic
    //void IComplexAndLogic.AddOther(IComplexOrLogic other)
    //    => _others.Add(other);
    //IEnumerable<IComplexOrLogic> IComplexAndLogic.Others
    //    => _others;
    //IComplexOrLogic IComplexAndLogic.Not()
    //    => this.Not();
    //#endregion
    //#region MergeTo
    //IAndLogic IAndLogic.MergeTo(AndLogic and)
    //    => this.MergeTo(and);
    //IAndLogic IAndLogic.MergeTo(ComplexAndLogic and)
    //    => this.MergeTo(and);
    //SqlAndQuery IAndLogic.MergeTo(SqlAndQuery query)
    //    => this.MergeTo(query);
    //ComplexOrLogic IAndLogic.MergeTo(ComplexOrLogic or)
    //    => this.MergeToOr(or);
    //IOrLogic IAndLogic.MergeTo(OrLogic or)
    //    => this.MergeToOr(or);
    //SqlOrQuery IAndLogic.MergeTo(SqlOrQuery query)
    //    => this.MergeToOr(query);
    //#endregion
    //#region Logic
    //internal override Logic ToAnd()
    //    => this;
    //internal override Logic ToOr()
    //{
    //    var preview = Preview();
    //    if (preview.IsEmpty)
    //        return new OrLogic();
    //    if (preview.HasSecond)
    //        return new ComplexOrLogic().AddOtherCore(this);
    //    return new OrLogic(preview.First);
    //}
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
    public static ComplexAndLogic operator &(ComplexAndLogic logic, AtomicLogic other)
        => logic.AndCore(other);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(ComplexAndLogic logic, IAndLogic other)
    //    => logic.And(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IAndLogic other, ComplexAndLogic logic)
    //    => other.And(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, AndLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, ComplexAndLogic other)
        => logic.AndCore(other);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static ComplexAndLogic operator &(ComplexAndLogic logic, IOrLogic other)
    //    => logic.And(other);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static ComplexAndLogic operator &(IOrLogic other, ComplexAndLogic logic)
    //    => logic.And(other);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, OrLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, ComplexOrLogic other)
        => logic.AndCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(ComplexAndLogic logic, Logic other)
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
    public static Logic operator |(ComplexAndLogic logic, AtomicLogic other)
        => logic.OrCore(other);
    #endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(ComplexAndLogic logic, IOrLogic other)
    //    => other.Or(logic);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IOrLogic other, ComplexAndLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(ComplexAndLogic logic, OrLogic other)
        => other.OrCore(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexAndLogic logic, ComplexOrLogic other)
        => other.OrCore(logic);
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(ComplexAndLogic logic, IAndLogic other)
    //    => logic.Or(other);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IAndLogic other, ComplexAndLogic logic)
    //    => other.Or(logic);
    //#endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexAndLogic logic, AndLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexAndLogic logic, ComplexAndLogic other)
        => logic.OrCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(ComplexAndLogic logic, Logic other)
        => logic.Or(other);
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator !(ComplexAndLogic logic)
        => logic.Not();
    #endregion
    #endregion
}
