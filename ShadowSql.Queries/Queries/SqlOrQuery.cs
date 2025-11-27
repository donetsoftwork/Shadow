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
    internal SqlOrQuery(ComplexOrLogic complex)
        : this(complex, new SqlConditionLogic(LogicSeparator.Or))
    {
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="complex"></param>
    /// <param name="conditions"></param>
    internal SqlOrQuery(ComplexOrLogic complex, SqlConditionLogic conditions)
        : base(complex, conditions)
    {
        _complex = complex;
    }
    #endregion
    #region 配置
    /// <summary>
    /// 复合逻辑
    /// </summary>
    private new readonly ComplexOrLogic _complex;
    /// <summary>
    /// 复合逻辑
    /// </summary>
    public ComplexOrLogic Complex
        => _complex;
    #endregion
    #region Or查询
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    public override SqlOrQuery Or(AtomicLogic atomic)
    {
        AddLogic(atomic);
        return this;
    }
    #endregion
    #region SqlQuery
    /// <inheritdoc/>
    public override SqlQuery CopyQuery()
        => this.Copy();
    /// <summary>
    /// Or转化为And(反转)
    /// </summary>
    /// <returns></returns>
    public override SqlAndQuery ToAnd()
        => new(this.MergeToAnd(new ComplexAndLogic()));
    /// <inheritdoc/>
    public override SqlOrQuery ToOr()
        => this;
    #endregion
}
