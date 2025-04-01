using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改表
/// </summary>
/// <param name="table"></param>
/// <param name="filter"></param>
public class TableUpdate<TTable>(TTable table, ISqlLogic filter)
    : UpdateBase<TTable>(table, filter)
    where TTable : ITable
{
    #region ISqlFragment
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.UpdatePrefix(sql);
        _source.Write(engine, sql);

        sql.Append(" SET ");
        var appended = false;
        foreach (var assign in _assignInfos)
        {
            if (appended)
                sql.Append(',');
            assign.Write(engine, sql);
            appended = true;
        }
        if (appended)
        {
            var point = sql.Length;
            engine.WherePrefix(sql);
            if(!_filter.TryWrite(engine, sql))
            {
                //回滚
                sql.Length = point;
            }
        }
        else
        {
            throw new InvalidOperationException("没有设置修改信息");
        }
    }
    #endregion
}
