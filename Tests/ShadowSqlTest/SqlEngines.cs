using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Engines.MySql;
using ShadowSql.Engines.Oracle;
using ShadowSql.Engines.Postgres;
using ShadowSql.Engines.Sqlite;

namespace ShadowSqlTest;

public class SqlEngines
{
    #region SqlEngines
    public static readonly ISqlEngine MsSql = new MsSqlEngine();
    public static readonly ISqlEngine MySql = new MySqlEngine();
    public static readonly ISqlEngine Sqlite = new SqliteEngine();
    public static readonly ISqlEngine Oracle = new OracleEngine();
    public static readonly ISqlEngine Postgres = new PostgresEngine();
    #endregion
    /// <summary>
    /// 获取数据库引擎
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static ISqlEngine Get(SqlEngineNames name)
    {
        return name switch
        {
            SqlEngineNames.MsSql => MsSql,
            SqlEngineNames.MySql => MySql,
            SqlEngineNames.Sqlite => Sqlite,
            SqlEngineNames.Oracle => Oracle,
            SqlEngineNames.Postgres => Postgres,
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
        };
    }}

public enum SqlEngineNames
{
    MsSql,
    MySql,
    Sqlite,
    Oracle,
    Postgres
}


