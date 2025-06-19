using ShadowSql.Logics;
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
    internal SqlAndQuery(ComplexAndLogic complex)
        : base(complex, new SqlConditionLogic(LogicSeparator.And))
    {
    }
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="complex"></param>
    /// <param name="conditions"></param>
    internal SqlAndQuery(ComplexAndLogic complex, SqlConditionLogic conditions)
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
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    public override SqlAndQuery And(AtomicLogic atomic)
    {
        AddLogic(atomic);
        return this;
    }
    #endregion
    #region SqlQuery
    /// <inheritdoc/>
    public override SqlQuery CopyQuery()
        => this.Copy();
    /// <inheritdoc/>
    public override SqlAndQuery ToAnd()
        => this;
    /// <summary>
    /// And转化为Or(反转)
    /// </summary>
    /// <returns></returns>
    public override SqlOrQuery ToOr()
        => new(this.MergeToOr(new ComplexOrLogic()));
    #endregion
}
