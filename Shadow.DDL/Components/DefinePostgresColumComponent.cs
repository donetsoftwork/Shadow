using Shadow.DDL.Schemas;
using ShadowSql.Engines;
using System.Text;

namespace Shadow.DDL.Components;

/// <summary>
/// 定义PostgreSQL列
/// </summary>
public class DefinePostgresColumComponent : IDefineColumComponent
{
    /// <summary>
    /// 写入的列定义
    /// </summary>
    /// <param name="column">列</param>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    public void WriteColumnSchema(ColumnSchema column, ISqlEngine engine, StringBuilder sql)
    {
        engine.Identifier(sql, column.Name);
        sql.Append(' ');
        if ((column.ColumnType & ColumnType.Computed) == ColumnType.Computed)
        {
            engine.ColumnAs(sql, column.SqlType);
            return;
        }
        sql.Append(column.SqlType);
        if ((column.ColumnType & ColumnType.Key) == ColumnType.Key)
        {
            sql.Append(" PRIMARY KEY");
        }
        if ((column.ColumnType & ColumnType.Identity) == ColumnType.Identity)
        {
            sql.Append(" SERIAL");
        }
        if ((column.ColumnType & ColumnType.NOTNULL) == ColumnType.NOTNULL)
        {
            sql.Append(" NOT NULL");
        }
        if (!string.IsNullOrWhiteSpace(column.Default))
        {
            sql.Append(" DEFAULT ").Append(column.Default);
        }
    }
}
