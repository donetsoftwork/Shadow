using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改表
/// </summary>
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class TableUpdate<TTable>(TTable table, ISqlLogic filter)
    : UpdateBase<TTable>(table)
    where TTable : IUpdateTable
{
    #region 配置
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
    /// <summary>
    /// 添加修改信息
    /// </summary>
    /// <param name="operation">操作</param>
    /// <returns></returns>
    public TableUpdate<TTable> Set(Func<TTable, IAssignInfo> operation)
    {
        SetCore(operation(_source));
        return this;
    }
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
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
