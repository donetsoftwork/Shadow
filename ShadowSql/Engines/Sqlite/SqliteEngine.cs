using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.Sqlite;

/// <summary>
/// Sqlite
/// </summary>
/// <param name="select"></param>
/// <param name="sqlVales"></param>
/// <param name="components"></param>
public class SqliteEngine(ISelectComponent select, ISqlValueComponent sqlVales, IPluginProvider? components)
    : EngineBase(select, sqlVales, components), ISqlEngine
{
    /// <summary>
    /// MsSql
    /// </summary>
    public SqliteEngine()
        : this(new SqliteSelectComponent(), new SqlValueComponent("1", "0", "NULL"), null)
    {
    }
    /// <summary>
    /// 标识符格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('\"').Append(name).Append('\"');
    }    
    /// <summary>
    /// 插入自增列sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool InsertedIdentity(StringBuilder sql)
    {
        sql.Append(";SELECT last_insert_rowid()");
        return true;
    }
    /// <summary>
    /// Sqlite不支持Truncate,使用Delete代替
    /// </summary>
    /// <param name="sql"></param>
    public override void TruncatePrefix(StringBuilder sql)
        => sql.Append("DELETE FROM ");
}
