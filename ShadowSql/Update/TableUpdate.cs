﻿using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改表
/// </summary>
/// <param name="table"></param>
/// <param name="filter"></param>
public class TableUpdate<TTable>(TTable table, ISqlLogic filter)
    : UpdateBase<TTable>(table)
    where TTable : ITable
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
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void Write(ISqlEngine engine, StringBuilder sql)
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
