using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using Shadow.DDL.SqlTypes;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Text;

namespace ShadowSql.Postgres;

/// <summary>
/// 定义PostgreSQL列
/// </summary>
public class DefinePostgresColumComponent()
    : DefineColumComponent(new PostgreSqlType())
{
    /// <inheritdoc/>
    public override bool WriteColumnSchema(ISqlEngine engine, StringBuilder sql, ColumnSchema column)
    {
        engine.Identifier(sql, column.Name);
        sql.Append(' ');

        var defaultValue = column.Default;
        if ((column.ColumnType & ColumnType.Computed) == ColumnType.Computed)
        {
            // 通过默认值配置计算列
            if (string.IsNullOrWhiteSpace(defaultValue))
                return false;
            column.SqlType.Write(engine, sql);
            sql.Append(" GENERATED ALWAYS AS ").Append(defaultValue).Append(" STORED");
            return true;
        }
        if ((column.ColumnType & ColumnType.Identity) == ColumnType.Identity)
        {
            // 检查SERIAL
            sql.Append(CheckSerial(engine, column.SqlType));
        }
        else
        {
            column.SqlType.Write(engine, sql);
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
    /// <summary>
    /// 检查SERIAL
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sqlType"></param>
    /// <returns></returns>
    public static string CheckSerial(ISqlEngine engine, ISqlType sqlType)
    {
        var serial = engine.Sql(sqlType);
        switch (serial)
        {
            case PostgreSqlType.SerialTypeName:
                return serial;
            case PostgreSqlType.BigSerialTypeName:
                return serial;
            case PostgreSqlType.SmallSerialTypeName:
                return serial;
            case PostgreSqlType.IntegerTypeName:
                return PostgreSqlType.SerialTypeName;
            case PostgreSqlType.BigIntTypeName:
                return PostgreSqlType.BigSerialTypeName;
            case PostgreSqlType.SmallIntTypeName:
                return PostgreSqlType.SmallSerialTypeName;
            default:
                break;
        }
        if (serial.Contains(PostgreSqlType.SerialTypeName, StringComparison.OrdinalIgnoreCase))
            return serial;
        return "SERIAL";
    }
    ///// <inheritdoc/>
    //public override bool WriteUniqueConstraint(ISqlEngine engine, StringBuilder sql, string name, IColumn[] columns)
    //{
    //    // CONSTRAINT uc_PersonID UNIQUE (Id,Name)
    //    var count = columns.Length;
    //    if (count == 0)
    //        return false;
    //    sql.Append(" UNIQUE (");
    //    columns[0].Write(engine, sql);
    //    for (var i = 1; i < count; i++)
    //    {
    //        sql.Append(',');
    //        columns[i].Write(engine, sql);
    //    }
    //    sql.Append(')');
    //    return true;
    //}
}
