using System.Collections.Generic;

namespace ShadowSql.Logics;

/// <summary>
/// 复合与逻辑
/// </summary>
public class ComplexAndLogic : ComplexLogicBase, IAndLogic, ISqlLogic
{
    /// <summary>
    /// 复合与逻辑
    /// </summary>
    public ComplexAndLogic()
        : this([], [])
    {
    }
    /// <summary>
    /// 复合与逻辑
    /// </summary>
    /// <param name="items"></param>
    /// <param name="others"></param>
    internal ComplexAndLogic(List<AtomicLogic> items, List<ComplexLogicBase> others)
        : base(LogicSeparator.And, items, others)
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
    #endregion
    /// <inheritdoc/>
    public override Logic ToAnd()
        => this;
    /// <inheritdoc/>
    public override Logic ToOr()
        => this.ToOrCore();
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
    public static ComplexAndLogic operator &(ComplexAndLogic logic, AtomicLogic other)
        => logic.AndCore(other);
    #endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, AndLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, ComplexAndLogic other)
        => logic.AndCore(other);
    #endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, OrLogic other)
        => logic.AndCore(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic logic, ComplexOrLogic other)
        => logic.AndCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
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
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(ComplexAndLogic logic, AtomicLogic other)
        => logic.OrCore(other);
    #endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(ComplexAndLogic logic, OrLogic other)
        => other.OrCore(logic);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexAndLogic logic, ComplexOrLogic other)
        => other.OrCore(logic);
    #endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexAndLogic logic, AndLogic other)
        => logic.OrCore(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexAndLogic logic, ComplexAndLogic other)
        => logic.OrCore(other);
    #endregion
    #region Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
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
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static ComplexOrLogic operator !(ComplexAndLogic logic)
        => logic.Not();
    #endregion
    #endregion
}
