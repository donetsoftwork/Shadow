using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 多条插入
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="items"></param>
public class MultiInsert<TTable>(TTable table, List<IInsertValues> items)
    : MultiInsertBase(items), IMultiInsert
    where TTable : IInsertTable
{
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <param name="table"></param>
    public MultiInsert(TTable table)
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
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiInsert<TTable> Insert(Func<TTable, InsertValues> select)
    {
        Add(select(_table));
        return this;
    }
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteInsert(_table, engine, sql);
}
