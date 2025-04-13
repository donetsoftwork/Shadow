using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入单条
/// </summary>
/// <param name="table"></param>
/// <param name="items"></param>
public class SingleInsert(IInsertTable table, List<IInsertValue> items)
    : SingleInsertBase(items), ISingleInsert
{
    /// <summary>
    /// 插入单条
    /// </summary>
    /// <param name="table"></param>
    public SingleInsert(IInsertTable table)
        : this(table, [])
    { 
    }
    /// <summary>
    /// 插入单条
    /// </summary>
    /// <param name="tableName"></param>
    public SingleInsert(string tableName)
    : this(SimpleTable.Use(tableName), [])
    {
    }
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected readonly IInsertTable _table = table;
    /// <summary>
    /// 源表
    /// </summary>
    public IInsertTable Table
        => _table;
    #endregion
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteInsert(_table, engine, sql);
}
