using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Sqlite;

/// <summary>
/// 定义SQLite列
/// </summary>
public class DefineSqliteColumComponent()
    : DefineColumComponent(new SqliteSqlType())
{
    /// <inheritdoc/>
    public override bool WriteColumnSchema(ISqlEngine engine, StringBuilder sql, ColumnSchema column)
    {
        // Sqlite不支持计算列,忽略
        if ((column.ColumnType & ColumnType.Computed) == ColumnType.Computed)
            return false;
        engine.Identifier(sql, column.Name);
        sql.Append(' ');
        column.SqlType.Write(engine, sql);
        if ((column.ColumnType & ColumnType.Key) == ColumnType.Key)
        {
            sql.Append(" PRIMARY KEY");
        }
        if ((column.ColumnType & ColumnType.Identity) == ColumnType.Identity)
        {
            sql.Append(" AUTOINCREMENT");
        }
        if ((column.ColumnType & ColumnType.Unique) == ColumnType.Unique)
        {
            sql.Append(" UNIQUE");
        }
        if ((column.ColumnType & ColumnType.NotNull) == ColumnType.NotNull)
        {
            sql.Append(" NOT NULL");
        }
        if (!string.IsNullOrWhiteSpace(column.Default))
        {
            sql.Append(" DEFAULT ").Append(column.Default);
        }
        return true;
    }
    /// <inheritdoc/>
    public override bool WriteUniqueConstraint(ISqlEngine engine, StringBuilder sql, string name, IColumn[] columns)
    {
        // CONSTRAINT uc_PersonID UNIQUE (Id,Name)
        if (string.IsNullOrWhiteSpace(name) || columns.Length == 0)
            return false;
        WriteUnique(engine, sql, columns);
        return true;
    }
}