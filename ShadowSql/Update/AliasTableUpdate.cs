using ShadowSql.AliasTables;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改别名表
/// </summary>
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class AliasTableUpdate<TTable>(AliasUpdateTable<TTable> table, ISqlLogic filter)
    : UpdateBase<AliasUpdateTable<TTable>>(table)
    where TTable : ITable
{
    /// <summary>
    /// 修改别名表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
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
    /// <inheritdoc/>
    protected override void WriteUpdate(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteUpdate(engine, sql);
        sql.Append(_source.Alias);
    }
    /// <inheritdoc/>
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
