using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段信息
/// </summary>
public sealed class AggregateFieldInfo(ICompareField target, string aggregate)
    : AggregateFieldInfoBase(aggregate, target), IAggregateField, ISqlEntity
{
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
    }
    IAggregateFieldAlias IAggregateField.As(string alias)
        => new AggregateAliasFieldInfo(_target, _aggregate, alias);

    string IAggregateField.TargetName
        => _target.ViewName;
}
