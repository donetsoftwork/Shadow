using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.Delete;

/// <summary>
/// 表数据删除
/// </summary>
/// <param name="table"></param>
/// <param name="filter"></param>
public class AliasTableDelete(IAliasTable table, ISqlLogic filter)
    : IDelete
{
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected IAliasTable _source = table;
    /// <summary>
    /// 源表
    /// </summary>
    public IAliasTable Source
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
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.DeletePrefix(sql);
        sql.Append(_source.Alias)
            .Append(" FROM ");
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
        //    if (!_filter.Write(engine, sql))
        //    {
        //        //回滚
        //        sql.Length = point;
        //    }
        //    return true;
        //}
        //return false;
    }
    #endregion
    #region IDelete
    ITableView IDelete.Source
        => _source;
    #endregion
}
