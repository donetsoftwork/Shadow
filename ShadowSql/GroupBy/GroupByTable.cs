using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对Table进行分组查询
/// </summary>
/// <param name="table"></param>
/// <param name="where"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public class GroupByTable<TTable>(TTable table, ISqlLogic where, IFieldView[] fields, SqlQuery having)
    : GroupByBase<TTable>(table, fields, having)
    where TTable : ITable
{
    /// <summary>
    /// 对TableQuery进行分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    public GroupByTable(TTable table, ISqlLogic where, IFieldView[] fields)
        :this(table, where, fields, SqlQuery.CreateAndQuery())
    {
    }
    #region 配置
    private readonly ISqlLogic _where = where;
    /// <summary>
    /// where查询条件
    /// </summary>
    public ISqlLogic Where
        => _where;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 数据源拼写(+WHERE)
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void AcceptSource(ISqlEngine engine, StringBuilder sql)
    {
        _source.Write(engine, sql);
        var point = sql.Length;
        //可选的WHERE
        engine.WherePrefix(sql);
        if (!_where.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
        //if (_source.Write(engine, sql))
        //{
        //    var point = sql.Length;
        //    //可选的WHERE
        //    engine.WherePrefix(sql);
        //    if (!_where.Write(engine, sql))
        //    {
        //        //回滚
        //        sql.Length = point;
        //    }
        //    return true;
        //}
        //return false;
    }
    #endregion
}
