using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// 或逻辑
/// </summary>
public sealed class OrLogic : Logic, IOrLogic, ISqlLogic
{
    /// <summary>
    /// 或逻辑
    /// </summary>
    public OrLogic()
    : this([])
    {
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    public OrLogic(AtomicLogic logic)
        : this([logic])
    {
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="items"></param>
    internal OrLogic(List<AtomicLogic> items)
        : base(LogicSeparator.Or, items)
    {
    }
    #region Logic
    #region 与逻辑
    /// <inheritdoc/>
    public override Logic And(AtomicLogic atomic)
        => this.AndCore(atomic);
    /// <inheritdoc/>
    public override Logic And(AndLogic and)
        => this.AndCore(and);
    /// <inheritdoc/>
    public override Logic And(ComplexAndLogic and)
        => this.AndCore(and);
    /// <inheritdoc/>
    public override Logic And(OrLogic or)
        => this.AndCore(or);
    /// <inheritdoc/>
    public override Logic And(ComplexOrLogic or)
        => this.AndCore(or);
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public override Logic And(Logic logic);
    #endregion
    #region 或逻辑
    /// <inheritdoc/>
    public override Logic Or(AtomicLogic atomic)
        => this.OrCore(atomic);
    /// <inheritdoc/>
    public override Logic Or(OrLogic or)
        => this.OrCore(or);
    /// <inheritdoc/>
    public override Logic Or(ComplexOrLogic or)
        => this.OrCore(or);
    /// <inheritdoc/>
    public override Logic Or(AndLogic and)
        => this.OrCore(and);
    /// <inheritdoc/>
    public override Logic Or(ComplexAndLogic and)
        => this.OrCore(and);
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public override Logic Or(Logic logic);
    #endregion
    /// <inheritdoc/>
    public override Logic ToAnd()
        => this.ToAndCore();
    /// <inheritdoc/>
    public override Logic ToOr()
        => this;
    #endregion
    #region Not
    /// <inheritdoc/>
    ISqlLogic ISqlLogic.Not()
        => this.NotLogic();
    #endregion
    #region 运算符重载
    #region And
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, AtomicLogic other)
        => logic.AndCore(other);
    #endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, AndLogic other)
        => other.AndCore(logic);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IAndLogic operator &(OrLogic logic, ComplexAndLogic other)
        => other.AndCore(logic);
    #endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, OrLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(OrLogic logic, ComplexOrLogic other)
        => logic.AndCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
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
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic operator |(OrLogic logic, AtomicLogic other)
        => logic.OrCore(other);
    #endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic operator |(OrLogic logic, OrLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(OrLogic logic, ComplexOrLogic other)
        => logic.OrCore(other);
    #endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(OrLogic logic, AndLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(OrLogic logic, ComplexAndLogic other)
        => logic.OrCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(OrLogic logic, Logic other)
        => logic.Or(other);
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static AndLogic operator !(OrLogic logic)
        => logic.Not();
    #endregion
    #endregion
}
