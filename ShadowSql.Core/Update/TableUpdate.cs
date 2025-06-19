using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改表
/// </summary>
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class TableUpdate(IUpdateTable table, ISqlLogic filter)
    : UpdateBase, IUpdate
{
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="filter">过滤条件</param>
    public TableUpdate(string tableName, ISqlLogic filter)
        : this(EmptyTable.Use(tableName), filter)
    {
    }
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected IUpdateTable _table = table;
    /// <summary>
    /// 源表
    /// </summary>
    public IUpdateTable Table
        => _table;
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
    #region UpdateBase
    /// <inheritdoc/>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _table.Write(engine, sql);
    /// <inheritdoc/>
    /// <exception cref="ArgumentException"></exception>
    internal override IAssignView GetAssignField(string fieldName)
        => _table.GetAssignField(fieldName)
        ?? throw new ArgumentException(fieldName + "字段不存在", nameof(fieldName));
    #endregion
    /// <summary>
    /// 按表名修改
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="filter">过滤条件</param>
    /// <returns></returns>
    public static TableUpdate Create(string tableName, ISqlLogic filter)
        => new(EmptyTable.Use(tableName), filter);
    #region ISqlEntity
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        WriteUpdate(engine, sql);
        WriteSource(engine, sql);
        WriteSet(engine, sql);
        var point = sql.Length;
        engine.WherePrefix(sql);
        if (!_filter.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
    }
    #endregion
}
