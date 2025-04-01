using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql.Tables;

/// <summary>
/// 逻辑查询表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="filter"></param>
public class TableQuery<TTable>(TTable source, Logic filter)
    : DataQuery<TTable>(source, filter), IWhere
    where TTable : ITable
{
    /// <summary>
    /// 逻辑查询表
    /// </summary>
    /// <param name="source"></param>
    public TableQuery(TTable source)
        : this(source, new AndLogic())
    {
    }
}
