using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段信息
/// </summary>
/// <param name="aggregate"></param>
/// <param name="columnName"></param>
public sealed class AggregateFieldInfo(string aggregate, string columnName)
    : AggregateFieldInfoBase(aggregate, columnName), IAggregateField, ISqlEntity
{
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_aggregate).Append('(');
        engine.Identifier(sql, _name);
        sql.Append(')');
        //return true;
    }
    string IAggregateField.TargetName
        => _name;
}
