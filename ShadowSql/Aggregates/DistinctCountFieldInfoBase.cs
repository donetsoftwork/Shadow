using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 去重统计字段信息
/// </summary>
public abstract class DistinctCountFieldInfoBase(IFieldView target)
    : IAggregate, ISqlEntity
{
    #region 配置
    /// <summary>
    /// 被去重的字段
    /// </summary>
    protected readonly IFieldView _target = target;
    /// <summary>
    /// 被去重的字段
    /// </summary>
    public IFieldView Target
        => _target;
    #endregion
    string IAggregate.Aggregate
        => AggregateConstants.Count;
    #region ISqlEntity
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(AggregateConstants.Count)
            .Append("(DISTINCT ");
        _target.Write(engine, sql);
        sql.Append(')');
    }
    #endregion
}
