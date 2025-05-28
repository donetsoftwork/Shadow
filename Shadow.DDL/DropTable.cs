using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using System.Text;

namespace Shadow.DDL;

/// <summary>
/// 删表
/// </summary>
/// <param name="table"></param>
public class DropTable(ITable table)
    : IExecuteSql
{
    /// <summary>
    /// 删表
    /// </summary>
    /// <param name="tableName"></param>
    public DropTable(string tableName)
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
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
        => WriteDropTable(engine, sql, _table);

    /// <summary>
    /// DROP TABLE
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static void WriteDropTable(ISqlEngine engine, StringBuilder sql, ITable table)
    {
        sql.Append("DROP TABLE ");
        table.Write(engine, sql);
    }
    /// <summary>
    /// DROP TABLE
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static void WriteDropTable(ISqlEngine engine, StringBuilder sql, string tableName)
    {
        sql.Append("DROP TABLE ");
        engine.Identifier(sql, tableName);
    }
}
