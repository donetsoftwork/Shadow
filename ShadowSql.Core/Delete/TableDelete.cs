using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System.Text;

namespace ShadowSql.Delete;

/// <summary>
/// 表数据删除
/// </summary>
/// <param name="table"></param>
/// <param name="filter"></param>
public class TableDelete(ITable table, ISqlLogic filter)
    : IDelete
{
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="filter"></param>
    public TableDelete(string tableName, ISqlLogic filter)
        : this(EmptyTable.Use(tableName), filter)
    {
    }
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected ITable _source = table;
    /// <summary>
    /// 源表
    /// </summary>
    public ITable Source
        => _source;
    /// <summary>
    /// 过滤条件
    /// </summary>
    protected readonly ISqlLogic _filter = filter;
    /// <summary>
    /// 过滤条件
    /// </summary>
    public ISqlLogic Filter
        => _filter;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.DeletePrefix(sql);
        sql.Append("FROM ");
        _source.Write(engine, sql);
        var point = sql.Length;
        engine.WherePrefix(sql);
        if (!_filter.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
    }
    #endregion
    #region IDelete
    ITableView IDelete.Source
        => _source;
    #endregion
}
