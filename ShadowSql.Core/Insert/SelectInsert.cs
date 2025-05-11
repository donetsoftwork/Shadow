using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入Select子查询
/// </summary>
/// <param name="table"></param>
/// <param name="columns"></param>
/// <param name="select"></param>
public class SelectInsert(IInsertTable table, List<IColumn> columns, ISelect select)
    : SelectInsertBase(columns, select), ISelectInsert
{
    /// <summary>
    /// 插入Select子查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="select"></param>
    public SelectInsert(IInsertTable table, ISelect select)
        : this(table, [], select)
    {
    }
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    public SelectInsert(string tableName, ISelect select)
        : this(new Table(tableName), select)
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
