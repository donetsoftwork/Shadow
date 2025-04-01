using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Variants;
using System;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改表
/// </summary>
/// <param name="table"></param>
/// <param name="filter"></param>
public class AliasTableUpdate<TTable>(TableAlias<TTable> table, ISqlLogic filter)
    : UpdateBase<TableAlias<TTable>>(table, filter)
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
        sql.Append(_source.Alias);
        //var assigns = GetAssigns();
        sql.Append(" SET ");
        var appended = false;
        foreach (var assign in _assignInfos)
        {
            //var point = sql.Length;
            if (appended)
                sql.Append(',');
            assign.Write(engine, sql);
            appended = true;
            //if (assign.Write(engine, sql))
            //{
            //    appended = true;
            //}
            //else
            //{
            //    //回滚
            //    sql.Length = point;
            //}
        }
        
        if (appended)
        {
            sql.Append(" FROM ");
            _source.Write(engine, sql);
            var point = sql.Length;
            engine.WherePrefix(sql);
            if (!_filter.TryWrite(engine, sql))
            {
                //回滚
                sql.Length = point;
            }
            //if (_source.Write(engine, sql))
            //{
            //    var point = sql.Length;
            //    engine.WherePrefix(sql);
            //    if (!_filter.TryWrite(engine, sql))
            //    {
            //        //回滚
            //        sql.Length = point;
            //    }
            //}
            //return true;
        }
        else
        {
            throw new InvalidOperationException("没有设置修改信息");
        }
        //return false;
    }
    #endregion
}
