using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.Postgres;

/// <summary>
/// Postgres
/// </summary>
/// <param name="select">筛选</param>
/// <param name="sqlValues"></param>
/// <param name="components"></param>
public class PostgresEngine(ISelectComponent select, ISqlValueComponent sqlValues, IPluginProvider? components)
    : EngineBase(select, sqlValues, components), ISqlEngine
{
    /// <summary>
    /// Postgres
    /// </summary>
    public PostgresEngine()
        : this(new PostgresSelectComponent() ,new SqlValueComponent("1", "0", "NULL"), null)
    {
    }
    /// <inheritdoc/>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('\"').Append(name).Append('\"');
    }
    /// <inheritdoc/>
    public override bool InsertedIdentity(StringBuilder sql)
    {
        sql.Append(";SELECT lastval()");
        return true;
    }
}
