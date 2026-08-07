using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Oracle;

/// <summary>
/// 定义Oracle列
/// </summary>
public class DefineOracleColumComponent()
    : DefineColumComponent(new OracleSqlType())
{
    /// <inheritdoc/>
    public override bool WriteColumnSchema(ISqlEngine engine, StringBuilder sql, ColumnSchema column)
    {
        engine.Identifier(sql, column.Name);
        sql.Append(' ');
        column.SqlType.Write(engine, sql);

        var defaultValue = column.Default;
        if ((column.ColumnType & ColumnType.Computed) == ColumnType.Computed)
        {
            // 通过默认值配置计算列
            if (string.IsNullOrWhiteSpace(defaultValue))
                return false;

            sql.Append(" GENERATED ALWAYS AS ").Append(defaultValue).Append(" VIRTUAL");
            return true;
        }
        if ((column.ColumnType & ColumnType.Key) == ColumnType.Key)
        {
            sql.Append(" PRIMARY KEY");
        }
        if ((column.ColumnType & ColumnType.Unique) == ColumnType.Unique)
        {
            sql.Append(" UNIQUE");
        }
        if ((column.ColumnType & ColumnType.NotNull) == ColumnType.NotNull)
        {
            sql.Append(" NOT NULL");
        }
        if (!string.IsNullOrWhiteSpace(defaultValue))
        {
            sql.Append(" DEFAULT ").Append(defaultValue);
        }
        return true;
    }
}
