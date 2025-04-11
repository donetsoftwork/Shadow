using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql.Insert;

/// <summary>
/// 插入数据基类
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
public abstract class InsertBase<TTable>(TTable table)
    : InsertBase(table.Name, [.. table.InsertColumns]), IInsert, IInsertTable
    where TTable : ITable
{
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
    #endregion
    IInsertTable IInsert.Table
        => this;
}
/// <summary>
/// 插入数据基类
/// </summary>
/// <param name="name"></param>
/// <param name="columns"></param>
public abstract class InsertBase(string name, IColumn[] columns)
    : Identifier(name)
{
    /// <summary>
    /// 可插入的列
    /// </summary>
    protected readonly IColumn[] _insertColumns = columns;
    /// <summary>
    /// 可插入的列
    /// </summary>
    public IEnumerable<IColumn> InsertColumns
        => _insertColumns;
}
