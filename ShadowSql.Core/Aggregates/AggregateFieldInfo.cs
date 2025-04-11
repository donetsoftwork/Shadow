using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段信息
/// </summary>
public sealed class AggregateFieldInfo
    : AggregateFieldInfoBase, IAggregateField, ISqlEntity
{
    internal AggregateFieldInfo(string aggregate, IFieldView target)
        : base(aggregate, target)
    {
    }
    /// <summary>
    /// 聚合列
    /// </summary>
    /// <param name="column"></param>
    /// <param name="aggregate"></param>
    public AggregateFieldInfo(IColumn column, string aggregate)
        :this(aggregate, column)
    {
    }
    /// <summary>
    /// 聚合字段
    /// </summary>
    /// <param name="field"></param>
    /// <param name="aggregate"></param>
    public AggregateFieldInfo(IField field, string aggregate)
        : this(aggregate, field)
    {
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        _target.Write(engine, sql);
        sql.Append(')');
    }
    string IAggregateField.TargetName
        => _target.ViewName;
}
