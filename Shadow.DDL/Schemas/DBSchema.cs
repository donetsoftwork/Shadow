using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 数据库
/// </summary>
/// <param name="engine">数据库引擎</param>
/// <param name="dbName">数据库名</param>
public class DBSchema(ISqlEngine engine, string dbName = "")
    : Identifier(dbName), IDB
{
    private readonly ISqlEngine _engine = engine;
    private readonly Dictionary<string, TableSchema> _tables = [];
    /// <summary>
    /// 表
    /// </summary>
    public IEnumerable<TableSchema> Tables
        => _tables.Values;
    /// <summary>
    /// 数据库引擎
    /// </summary>
    public ISqlEngine Engine 
        => _engine;
    /// <summary>
    /// 添加表
    /// </summary>
    /// <param name="table">表</param>
    public void AddTable(TableSchema table)
    {
        var sqlName = _engine.Sql(table);
        if (_tables.ContainsKey(sqlName))
            return;
        _tables[sqlName] = table;
    }
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="sqlName"></param>
    /// <returns></returns>
    public TableSchema From(string sqlName)
    {
        if (_tables.TryGetValue(sqlName, out var table))
            return table;
        foreach (var item in _tables.Values)
        {
            if (item.Name == sqlName)
                return item;
        }
        throw new ArgumentException("Not found", sqlName);
    } 
    ITable IDB.From(string sqlName)
        => From(sqlName);
}
