using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Variants;

/// <summary>
/// 聚合(计算)列基类
/// </summary>
/// <typeparam name="TColumn"></typeparam>
/// <param name="aggregate"></param>
/// <param name="target"></param>
public class AggregateColumnBase<TColumn>(string aggregate, TColumn target)
    : IAggregate
    where TColumn : IColumn
{
    /// <summary>
    /// 聚合方法名
    /// </summary>
    protected readonly string _aggregate = aggregate;
    /// <summary>
    /// 原始列
    /// </summary>
    protected readonly TColumn _target = target;

    /// <summary>
    /// 聚合方法名
    /// </summary>
    public string Aggregate
        => _aggregate;
    /// <summary>
    /// 原始列
    /// </summary>
    public TColumn Target
        => _target;

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        //var point = sql.Length;
        sql.Append(_aggregate);
        sql.Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
        //if (_target.Write(engine, sql))
        //{
        //    sql.Append(')');
        //    return true;
        //}
        ////回滚
        //sql.Length = point;
        //return false;
    }
}
