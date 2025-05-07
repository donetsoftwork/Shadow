using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Filters;

/// <summary>
/// 过滤基类
/// </summary>
public abstract class FilterBase : TableViewBase
{   
    #region ISqlEntity
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void WriteSource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 拼写过滤条件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract bool WriteFilter(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 筛选前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual void FilterPrefix(ISqlEngine engine, StringBuilder sql)
        => engine.WherePrefix(sql);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        WriteSource(engine, sql);
        var point = sql.Length;
        FilterPrefix(engine, sql);
        if (!WriteFilter(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
    }
    #endregion
}
