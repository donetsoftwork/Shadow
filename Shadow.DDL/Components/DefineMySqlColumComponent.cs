﻿using Shadow.DDL.Schemas;
using ShadowSql.Engines;
using System.Text;

namespace Shadow.DDL.Components;

/// <summary>
/// 定义MySQL列
/// </summary>
public class DefineMySqlColumComponent : IDefineColumComponent
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="column"></param>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
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
            sql.Append(" AUTO_INCREMENT");
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
