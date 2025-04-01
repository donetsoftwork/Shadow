using ShadowSql.Engines;
using ShadowSql.Generators;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using System;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 多表查询
/// </summary>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public class MultiTableQuery(IIdentifierGenerator aliasGenerator, SqlQuery filter)
    : MultiTableBase(aliasGenerator, filter), IMultiTableQuery
{
    /// <summary>
    /// 多表视图
    /// </summary>
    public MultiTableQuery()
        : this(new IdIncrementGenerator("t"), SqlQuery.CreateAndQuery())
    {
    }
    #region ISqlEntity
    /// <summary>
    /// 拼写多表数据源sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void AcceptSource(ISqlEngine engine, StringBuilder sql)
    {
        if (_tables.Count == 0)
            throw new InvalidOperationException("没有数据表");
        bool appended = false;
        foreach (var member in _tables)
        {
            //var point = sql.Length;
            if (appended)
                sql.Append(',');
            member.Write(engine, sql);
            appended = true;       
        }      
    }
    #endregion
}
