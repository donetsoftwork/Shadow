using Shadow.DDL.Constraints;
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
/// <param name="table">表</param>
/// <param name="columns">列</param>
/// <param name="constraints">约束</param>
public class CreateTable(ITable table, IEnumerable<ColumnSchema> columns, IEnumerable<IConstraint> constraints)
    : IExecuteSql
{
    /// <summary>
    /// 建表
    /// </summary>
    /// <param name="table">表</param>
    public CreateTable(TableSchema table)
        : this(table, table.Columns, table.Constraints)
    {
    }
    #region 配置
    private readonly ITable _table = table;
    private readonly IEnumerable<ColumnSchema> _columns = columns;
    private readonly IEnumerable<IConstraint> _constraints = constraints;

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
    /// <summary>
    /// 约束
    /// </summary>
    public IEnumerable<IConstraint> Constraint
        => _constraints;
    #endregion

    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
        => WriteCreateTable(engine, sql, _table, _columns, _constraints);

    /// <summary>
    /// CREATE TABLE
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <param name="constraints">约束</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">请先注册定义列组件</exception>
    public static void WriteCreateTable(ISqlEngine engine, StringBuilder sql, ITable table, IEnumerable<ColumnSchema> columns, IEnumerable<IConstraint> constraints)
    {
        var columComponent = engine.GetDefineColumComponent();
        sql.Append("CREATE TABLE ");
        table.Write(engine, sql);
        sql.Append('(');
        var appended = false;
        var point = sql.Length;
        foreach (var column in columns)
        {
            if (appended)
            {
                point = sql.Length;
                sql.Append(',');
            }                
            appended = columComponent.WriteColumnSchema(engine, sql, column);
        }
        // 最后一次添加失败,回滚到上次添加之前
        if (!appended)
            sql.Length = point;        
        foreach (var constraint in constraints)
        {
            point = sql.Length;
            sql.Append(',');
            if (constraint.TryWrite(engine, sql))
                continue;
            // 写失败回滚
            sql.Length = point;
        }
        sql.Append(')');
    }
    /// <summary>
    /// CREATE TABLE
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">请先注册定义列组件</exception>
    public static void WriteCreateTable(ISqlEngine engine, StringBuilder sql, TableSchema table)
        => WriteCreateTable(engine, sql, table, table.Columns, table.Constraints);
}
