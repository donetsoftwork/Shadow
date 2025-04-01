using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 多表视图筛选列
/// </summary>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
public class MultiTableSelect(IMultiView multiTable, MultiTableFields fields)
    : SelectBase<IMultiView, MultiTableFields>(multiTable, fields)
{
    /// <summary>
    /// 多表视图筛选列
    /// </summary>
    /// <param name="multiTable"></param>
    public MultiTableSelect(IMultiView multiTable)
        : this(multiTable, new MultiTableFields(multiTable))
    {
    }
}
/// <summary>
/// 多表视图范围(分页)及列筛选
/// </summary>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public class MultiTableFetchSelect(MultiTableFetch fetch, MultiTableFields fields)
    : SelectBase<IFetch, MultiTableFields>(fetch, fields)
{
    /// <summary>
    /// 多表视图范围(分页)及列筛选
    /// </summary>
    /// <param name="fetch"></param>
    public MultiTableFetchSelect(MultiTableFetch fetch)
        : this(fetch, new MultiTableFields(fetch.Source))
    {
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
}
