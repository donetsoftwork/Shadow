using ShadowSql.Logics;
using System.Collections.Generic;

namespace ShadowSql.Queries;

/// <summary>
/// Or查询
/// </summary>
public class SqlOrQuery : SqlQuery, ISqlLogic
{
    #region SqlOrQuery
    /// <summary>
    /// Or查询
    /// </summary>
    public SqlOrQuery()
        : this(new ComplexOrLogic(), new SqlConditionLogic(LogicSeparator.Or))
    {
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="complex"></param>
    internal SqlOrQuery(ComplexLogicBase complex)
        : base(complex, new SqlConditionLogic(LogicSeparator.Or))
    {
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="complex"></param>
    /// <param name="conditions"></param>
    internal SqlOrQuery(ComplexLogicBase complex, SqlConditionLogic conditions)
        : base(complex, conditions)
    {
    }
    #endregion
    /// <summary>
    /// 复合逻辑
    /// </summary>
    public ComplexOrLogic Complex
        => (ComplexOrLogic)_complex;
    #region Or查询
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// query.Or("Id=@Id", "Status=@Status");
    /// </code>
    /// </example>
    public override SqlOrQuery Or(params IEnumerable<string> conditions)
    {
        AddConditions(conditions);
        return this;
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public override SqlOrQuery Or(AtomicLogic atomic)
    {
        AddLogic(atomic);
        return this;
    }
    ///// <summary>
    ///// Or查询
    ///// </summary>
    ///// <param name="or"></param>
    ///// <returns></returns>
    //public SqlOrQuery Or(IOrLogic or)
    //    => or.MergeTo(this);
    ///// <summary>
    ///// Or查询
    ///// </summary>
    ///// <param name="and"></param>
    ///// <returns></returns>
    //public SqlOrQuery Or(IAndLogic and)
    //    => and.MergeTo(this);
    #endregion
    #region SqlQuery
    /// <summary>
    /// 复制查询
    /// </summary>
    /// <returns></returns>
    public override SqlQuery CopyQuery()
        => this.Copy();
    /// <summary>
    /// Or转化为And(反转)
    /// </summary>
    /// <returns></returns>
    public override SqlAndQuery ToAnd()
        => new(this.MergeToAnd(new ComplexAndLogic()));
    /// <summary>
    /// Or
    /// </summary>
    /// <returns></returns>
    public override SqlOrQuery ToOr()
        => this;
    #endregion
    //#region IOrLogic
    //IOrLogic IOrLogic.Or(AtomicLogic atomic)
    //    => Or(atomic);
    //IOrLogic IOrLogic.Or(IOrLogic or)
    //    => Or(or);
    //IOrLogic IOrLogic.Or(IAndLogic and)
    //    => and.MergeTo(this);
    //IAndLogic IOrLogic.ToAnd()
    //    => ToAnd();
    //IAndLogic IOrLogic.Not()
    //    => this.Not();
    //ISqlLogic ISqlLogic.Not()
    //{
    //    var preview = Preview();
    //    if (preview.IsEmpty)
    //        return EmptyLogic.Instance;
    //    if (preview.HasSecond)
    //        return this.Not();
    //    return preview.First.Not();
    //}
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
}
