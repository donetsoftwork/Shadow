using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 多条插入
/// </summary>
/// <param name="table"></param>
/// <param name="items"></param>
public class MultiInsert(IInsertTable table, List<IInsertValues> items)
    : MultiInsertBase(items), IMultiInsert
{
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <param name="table"></param>
    public MultiInsert(IInsertTable table)
        : this(table, [])
    {
    }
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <param name="tableName"></param>
    public MultiInsert(string tableName)
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
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteInsert(_table, engine, sql);
}
