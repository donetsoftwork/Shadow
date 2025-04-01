using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.SelectFields;
using ShadowSql.Variants;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 别名表筛选单列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="fields"></param>
public class AliasTableSingleSelect<TTable>(ITableView source, AliasTableFields<TTable> fields)
    : SingleSelectBase<ITableView, AliasTableFields<TTable>>(source, fields)
    where TTable : ITable
{
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <param name="source"></param>
    public AliasTableSingleSelect(TableAlias<TTable> source)
        : this(source, new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <param name="source"></param>
    /// <param name="where"></param>
    public AliasTableSingleSelect(TableAlias<TTable> source, ISqlLogic where)
        : this(new TableFilter<TableAlias<TTable>>(source, where), new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <param name="filter"></param>
    public AliasTableSingleSelect(TableFilter<TableAlias<TTable>> filter)
        : this(filter, new AliasTableFields<TTable>(filter.Source))
    {
    }
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <param name="query"></param>
    public AliasTableSingleSelect(AliasTableQuery<TTable> query)
        : this(query, new AliasTableFields<TTable>(query.Source))
    {
    }
}
///// <summary>
///// 别名表过滤筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="filter"></param>
//public sealed class AliasTableFilterSingleSelect<TTable>(TableFilter<TableAlias<TTable>> filter)
//    : SingleSelectBase<ITableView, AliasTableFields<TTable>>(filter, new AliasTableFields<TTable>(filter.Source))
//    where TTable : ITable
//{
//}
///// <summary>
///// 别名表(及查询)筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="query"></param>
//public class AliasTableQuerySingleSelect<TTable>(AliasTableQuery<TTable> query)
//    : SingleSelectBase<ITableView, AliasTableFields<TTable>>(query, new AliasTableFields<TTable>(query.Source))
//    where TTable : ITable
//{
//}
/// <summary>
/// 别名表范围(分页)及列筛选单列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public class AliasTableFetchSingleSelect<TTable>(IFetch fetch, AliasTableFields<TTable> fields)
    : SingleSelectBase<IFetch, AliasTableFields<TTable>>(fetch, fields)
    where TTable : ITable
{
    /// <summary>
    /// 别名表范围(分页)及列筛选单列
    /// </summary>
    /// <param name="fetch"></param>
    public AliasTableFetchSingleSelect(AliasTableFetch<TTable> fetch)
        : this(fetch, new AliasTableFields<TTable>(fetch.Source))
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
