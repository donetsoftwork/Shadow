using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.Sqlite;

/// <summary>
/// Sqlite
/// </summary>
/// <param name="select">筛选</param>
/// <param name="sqlValues"></param>
/// <param name="components"></param>
public class SqliteEngine(ISelectComponent select, ISqlValueComponent sqlValues, IPluginProvider? components)
    : EngineBase(select, sqlValues, components), ISqlEngine
{
    /// <summary>
    /// MsSql
    /// </summary>
    public SqliteEngine()
        : this(new SqliteSelectComponent(), new SqlValueComponent("1", "0", "NULL"), null)
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
        sql.Append(";SELECT last_insert_rowid()");
        return true;
    }
    /// <summary>
    /// Sqlite不支持Truncate,使用Delete代替
    /// </summary>
    /// <param name="sql">sql</param>
    public override void TruncatePrefix(StringBuilder sql)
        => sql.Append("DELETE FROM ");
}
