using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入单条
/// </summary>
/// <param name="table">表</param>
/// <param name="items"></param>
public class SingleInsert(IInsertTable table, List<IInsertValue> items)
    : SingleInsertBase(items), ISingleInsert
{
    /// <summary>
    /// 插入单条
    /// </summary>
    /// <param name="table">表</param>
    public SingleInsert(IInsertTable table)
        : this(table, [])
    { 
    }
    /// <summary>
    /// 插入单条
    /// </summary>
    /// <param name="tableName">表名</param>
    public SingleInsert(string tableName)
    : this(EmptyTable.Use(tableName), [])
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
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteInsert(_table, engine, sql);
}
