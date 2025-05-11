using ShadowSql.AliasTables;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改别名表
/// </summary>
/// <param name="table"></param>
/// <param name="filter"></param>
public class AliasTableUpdate<TTable>(AliasUpdateTable<TTable> table, ISqlLogic filter)
    : UpdateBase<AliasUpdateTable<TTable>>(table)
    where TTable : IUpdateTable
{
    /// <summary>
    /// 修改别名表
    /// </summary>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    public AliasTableUpdate(IAliasTable<TTable> table, ISqlLogic filter)
        : this(new AliasUpdateTable<TTable>(table), filter)
    {
    }
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
    /// 拼写Update子句
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteUpdate(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteUpdate(engine, sql);
        sql.Append(_source.Alias);
    }
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(" FROM ");
        base.WriteSource(engine, sql);
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
