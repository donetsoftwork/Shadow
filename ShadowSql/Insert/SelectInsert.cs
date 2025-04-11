using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入Select子查询
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="columns"></param>
/// <param name="select"></param>
public class SelectInsert<TTable>(TTable table, List<IColumn> columns, ISelect select)
    : SelectInsertBase(columns, select), ISelectInsert
    where TTable : IInsertTable
{
    /// <summary>
    /// 插入Select子查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="select"></param>
    public SelectInsert(TTable table, ISelect select)
        : this(table, [], select)
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
    /// 设置需要插入的列
    /// </summary>
    /// <returns></returns>
    public SelectInsert<TTable> Insert(Func<TTable, IColumn> select)
    {
        Add(select(_table));
        return this;
    }
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteInsert(_table, engine, sql);
}
