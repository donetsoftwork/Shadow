using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Delete;

/// <summary>
/// 清空表
/// </summary>
/// <param name="table"></param>
public class TruncateTable(ITable table)
    : IExecuteSql
{
    #region 配置
    private readonly ITable _table = table;
    /// <summary>
    /// 表
    /// </summary>
    public ITable Table 
        => _table;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
        => WriteTruncateTable(engine, sql, _table);

    /// <summary>
    /// TRUNCATE TABLE
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static void WriteTruncateTable(ISqlEngine engine, StringBuilder sql, ITable table)
    {
        engine.TruncatePrefix(sql);
        table.Write(engine, sql);
    }
    /// <summary>
    /// TRUNCATE TABLE
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static void WriteTruncateTable(ISqlEngine engine, StringBuilder sql, string tableName)
    {
        engine.TruncatePrefix(sql);
        engine.Identifier(sql, tableName);
    }
}
