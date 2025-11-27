using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.Oracle;

/// <summary>
/// Oracle
/// </summary>
/// <param name="select">筛选</param>
/// <param name="sqlValues"></param>
/// <param name="components"></param>
public class OracleEngine(ISelectComponent select, ISqlValueComponent sqlValues, IPluginProvider? components)
    : EngineBase(select, sqlValues, components), ISqlEngine
{
    /// <summary>
    /// Oracle
    /// </summary>
    public OracleEngine()
        : this(new OracleSelectComponent(), new SqlValueComponent("1", "0", "NULL"), null)
    {
    }
    /// <inheritdoc/>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('\"').Append(name).Append('\"');
    }
    /// <inheritdoc/>
    public override void Parameter(StringBuilder sql, string name)
    {
        sql.Append(':').Append(name);
    }
    /// <inheritdoc/>
    public override void ColumnAs(StringBuilder sql, string alias)
    {
        sql.Append(' ').Append(alias);
    }
    /// <inheritdoc/>
    public override void TableAs(StringBuilder sql, string alias)
    {
        sql.Append(' ').Append(alias);
    }
    /// <inheritdoc/>
    public override void InsertMultiPrefix(StringBuilder sql)
    {
        sql.Append("INSERT ALL INTO ");
    }
    /// <summary>
    /// 每个表定义不同SEQUENCE来自增,不好通用
    /// </summary>
    /// <param name="sql">sql</param>
    /// <returns></returns>
    public override bool InsertedIdentity(StringBuilder sql)
        => false;
}
