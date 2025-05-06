using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Linq;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 多表(联表)修改
/// </summary>
/// <param name="multiTable"></param>
/// <param name="table"></param>
public class MultiTableUpdate(IMultiView multiTable, IAliasTable table)
    : UpdateBase, IUpdate
{
    /// <summary>
    /// 多表(联表)修改
    /// </summary>
    /// <param name="multiTable"></param>
    public MultiTableUpdate(IMultiView multiTable)
        : this(multiTable, multiTable.Tables.First())
    {
    }
    #region 配置
    /// <summary>
    /// 被删除的表
    /// </summary>
    internal IAliasTable _table = table;
    /// <summary>
    /// 被删除的表
    /// </summary>
    public IAliasTable Table
        => _table;
    private readonly IMultiView _multiTable = multiTable;
    /// <summary>
    /// 多表(联表)视图
    /// </summary>
    public IMultiView MultiTable 
        => _multiTable;
    ITableView IUpdate.Table
        => _table;
    #endregion
    #region UpdateBase
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    protected override IColumn? GetColumn(string columName)
        => _multiTable.GetColumn(columName);
    #endregion
    //public MultiTableUpdate Set<TAliasTable>(Func<TAliasTable, IAssignInfo> operation)
    //    where TAliasTable : IAliasTable
    //{
    //    _multiTable.From< TAliasTable >("")
    //    return this;
    //}
    #region ISqlEntity
        /// <summary>
        /// 拼写Update子句
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="sql"></param>
    protected override void WriteUpdate(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteUpdate(engine, sql);
        sql.Append(_table.Alias);
    }
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(" FROM ");
        _multiTable.Write(engine, sql);
    }
    #endregion
}
