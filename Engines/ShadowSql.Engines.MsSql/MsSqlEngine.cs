using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.MsSql;

/// <summary>
/// MsSql
/// </summary>
/// <param name="select">筛选</param>
/// <param name="sqlValues"></param>
/// <param name="components"></param>
public class MsSqlEngine(ISelectComponent select, ISqlValueComponent sqlValues, IPluginProvider? components) 
    : EngineBase(select, sqlValues, components), ISqlEngine
{
    /// <summary>
    /// MsSql
    /// </summary>
    public MsSqlEngine()
        : this(new MsSqlSelectComponent(), new SqlValueComponent("1", "0", "NULL"), null)
    {
    }
    /// <inheritdoc/>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('[').Append(name).Append(']');
    }
    /// <inheritdoc/>
    public override bool InsertedIdentity(StringBuilder sql)
    {
        sql.Append(";SELECT scope_identity()");
        return true;
    }
}
