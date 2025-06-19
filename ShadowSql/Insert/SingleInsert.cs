using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入单条
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table">表</param>
/// <param name="items"></param>
public class SingleInsert<TTable>(TTable table, List<IInsertValue> items)
    : SingleInsertBase(items), ISingleInsert
    where TTable : IInsertTable
{
    /// <summary>
    /// 插入单条
    /// </summary>
    /// <param name="table">表</param>
    public SingleInsert(TTable table)
        : this(table, [])
    {
    }
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected readonly TTable _table = table;
    /// <summary>
    /// 源表
    /// </summary>
    public TTable Table
        => _table;
    IInsertTable IInsert.Table
        => _table;
    #endregion
    /// <summary>
    /// 增加插入值
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public SingleInsert<TTable> Insert(Func<TTable, IInsertValue> select)
    {
        Add(select(_table));
        return this;
    }
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteInsert(_table, engine, sql);
}
