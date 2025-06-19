using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using System.Text;

namespace ShadowSql.Delete;

/// <summary>
/// 清空表
/// </summary>
/// <param name="table">表</param>
public class TruncateTable(ITable table)
    : IExecuteSql
{
    /// <summary>
    /// 清空表
    /// </summary>
    /// <param name="tableName">表名</param>
    public TruncateTable(string tableName)
        : this(EmptyTable.Use(tableName))
    {
    }
    #region 配置
    private readonly ITable _table = table;
    /// <summary>
    /// 表
    /// </summary>
    public ITable Table 
        => _table;
    #endregion
    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
        => WriteTruncateTable(engine, sql, _table);

    /// <summary>
    /// TRUNCATE TABLE
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static void WriteTruncateTable(ISqlEngine engine, StringBuilder sql, ITable table)
    {
        engine.TruncatePrefix(sql);
        table.Write(engine, sql);
    }
    /// <summary>
    /// TRUNCATE TABLE
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static void WriteTruncateTable(ISqlEngine engine, StringBuilder sql, string tableName)
    {
        engine.TruncatePrefix(sql);
        engine.Identifier(sql, tableName);
    }
}
