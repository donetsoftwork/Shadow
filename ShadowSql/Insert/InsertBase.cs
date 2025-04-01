using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql.Insert;

/// <summary>
/// 插入数据基类
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
public abstract class InsertBase<TTable>(TTable table)
    : Identifier(table.Name), IInsert, IInsertTable
    where TTable : ITable
{
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected readonly TTable _table = table;
    /// <summary>
    /// 可插入的列
    /// </summary>
    protected readonly IColumn[] _insertColumns = [.. table.InsertColumns];
    /// <summary>
    /// 源表
    /// </summary>
    public TTable Table
        => _table;
    #endregion
    IInsertTable IInsert.Table
        => this;
    IEnumerable<IColumn> IInsertTable.InsertColumns
        => _insertColumns;    
}
