using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shadow.DDL;

/// <summary>
/// 建表
/// </summary>
/// <param name="table"></param>
/// <param name="columns"></param>
public class CreateTable(ITable table, IEnumerable<ColumnSchema> columns)
    : IExecuteSql
{
    /// <summary>
    /// 建表
    /// </summary>
    /// <param name="table"></param>
    public CreateTable(TableSchema table)
        : this(table, table.Columns)
    {
    }
    #region 配置
    private readonly ITable _table = table;
    private readonly IEnumerable<ColumnSchema> _columns = columns;

    /// <summary>
    /// 表
    /// </summary>
    public ITable Table 
        => _table;
    /// <summary>
    /// 列
    /// </summary>
    public IEnumerable<ColumnSchema> Columns
        => _columns;
    #endregion

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
        => WriteCreateTable(engine, sql, _table, _columns);

    /// <summary>
    /// CREATE TABLE
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="table"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">请先注册定义列组件</exception>
    public static void WriteCreateTable(ISqlEngine engine, StringBuilder sql, ITable table, IEnumerable<ColumnSchema> columns)
    {
        var columComponent = engine.PluginProvider?.GetPlugin<IDefineColumComponent>()
            ?? throw new NotSupportedException("请先注册定义列组件");
        sql.Append("CREATE TABLE ");
        table.Write(engine, sql);
        sql.Append('(');
        var appended = false;
        foreach (var column in columns)
        {
            if (appended)
                sql.Append(',');
            columComponent.WriteColumnSchema(column, engine, sql);
            appended = true;
        }
        sql.Append(')');
    }
    /// <summary>
    /// CREATE TABLE
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">请先注册定义列组件</exception>
    public static void WriteCreateTable(ISqlEngine engine, StringBuilder sql, TableSchema table)
        => WriteCreateTable(engine, sql, table, table.Columns);

}
