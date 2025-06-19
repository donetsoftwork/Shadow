using Shadow.DDL.Schemas;
using ShadowSql.Delete;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace Shadow.DDL;

/// <summary>
/// 扩展方法
/// </summary>
public static class DDLServices
{
    /// <summary>
    /// 建表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static CreateTable ToCreate(this ITable table, IEnumerable<ColumnSchema> columns)
        => new(table, columns);
    /// <summary>
    /// 建表
    /// </summary>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static CreateTable ToCreate(this TableSchema table)
    => new(table, table.Columns);
    /// <summary>
    /// 删表
    /// </summary>
    /// <param name="table">表</param>
    public static DropTable ToDrop(this ITable table)
        => new(table);
}
