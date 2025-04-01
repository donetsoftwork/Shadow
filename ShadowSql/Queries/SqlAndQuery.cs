using ShadowSql.Logics;
using System;
using System.Collections.Generic;

namespace ShadowSql.Queries;

/// <summary>
/// And查询
/// </summary>
public class SqlAndQuery : SqlQuery, ISqlLogic
{
    #region SqlAndQuery
    /// <summary>
    /// And查询
    /// </summary>
    public SqlAndQuery()
        : this(new ComplexAndLogic(), new SqlConditionLogic(LogicSeparator.And))
    {
    }
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="complex"></param>
    internal SqlAndQuery(ComplexLogicBase complex)
        : base(complex, new SqlConditionLogic(LogicSeparator.And))
    {
    }
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="complex"></param>
    /// <param name="conditions"></param>
    internal SqlAndQuery(ComplexLogicBase complex, SqlConditionLogic conditions)
        : base(complex, conditions)
    {
    }
    #endregion
    /// <summary>
    /// 复合逻辑
    /// </summary>
    public ComplexAndLogic Complex
        => (ComplexAndLogic)_complex;
    #region And查询
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// query.And("Id=@Id", "Status=@Status");
    /// </code>
    /// </example>
    public override SqlAndQuery And(params IEnumerable<string> conditions)
    {
        AddConditions(conditions);
        return this;
    }
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public override SqlAndQuery And(AtomicLogic atomic)
    {
        AddLogic(atomic);
        return this;
    }

    ///// <summary>
    ///// And查询
    ///// </summary>
    ///// <param name="and"></param>
    ///// <returns></returns>
    //public SqlAndQuery And(IAndLogic and)
    //    => and.MergeTo(this);
    ///// <summary>
    ///// And查询
    ///// </summary>
    ///// <param name="or"></param>
    ///// <returns></returns>
    //public SqlAndQuery And(IOrLogic or)
    //    => or.MergeTo(this);
    #endregion
    #region SqlQuery
    /// <summary>
    /// 复制查询
    /// </summary>
    /// <returns></returns>
    public override SqlQuery CopyQuery()
        => this.Copy();
    /// <summary>
    /// And查询
    /// </summary>
    /// <returns></returns>
    public override SqlAndQuery ToAnd()
        => this;
    /// <summary>
    /// And转化为Or(反转)
    /// </summary>
    /// <returns></returns>
    public override SqlOrQuery ToOr()
        => new(this.MergeToOr(new ComplexOrLogic()));
    #endregion
    //#region IAndLogic
    //IAndLogic IAndLogic.And(AtomicLogic atomic)
    //    => And(atomic);
    //IAndLogic IAndLogic.And(IAndLogic and)
    //    => And(and);
    //IAndLogic IAndLogic.And(IOrLogic or)
    //    => And(or);
    //IOrLogic IAndLogic.ToOr()
    //    => ToOr();
    //IOrLogic IAndLogic.Not()
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
    //IOrLogic IAndLogic.MergeTo(OrLogic or)
    //    => this.MergeToOr(or);
    //ComplexOrLogic IAndLogic.MergeTo(ComplexOrLogic or)
    //    => this.MergeToOr(or);
    //SqlOrQuery IAndLogic.MergeTo(SqlOrQuery query)
    //    => this.MergeToOr(query);
    //#endregion
}
