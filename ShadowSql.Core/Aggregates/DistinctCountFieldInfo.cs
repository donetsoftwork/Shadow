using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 去重统计字段信息
/// </summary>
public class DistinctCountFieldInfo
    : DistinctCountFieldInfoBase, IDistinctCountField
{
    internal DistinctCountFieldInfo(IFieldView field)
         : base(field)
    {
    }
    /// <summary>
    /// 按字段去重统计字段信息
    /// </summary>
    /// <param name="field"></param>
    public DistinctCountFieldInfo(IField field)
        : base(field)
    {
    }
    /// <summary>
    /// 按列去重统计字段信息
    /// </summary>
    /// <param name="column"></param>
    public DistinctCountFieldInfo(IColumn column)
        : base(column)
    {
    }
    string IAggregateField.TargetName
        => _target.ViewName;

    #region ISqlEntity
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(AggregateConstants.Count)
            .Append("(DISTINCT ");
        _target.Write(engine, sql);
        sql.Append(')');
    }
    #endregion
}
