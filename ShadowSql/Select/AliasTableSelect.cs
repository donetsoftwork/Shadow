using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Variants;

namespace ShadowSql.Select;

/// <summary>
/// 别名表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
public sealed class AliasTableSelect<TTable>
    : SelectBase<ITableView, IAliasTable<TTable>>
    where TTable : ITable
{
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    internal AliasTableSelect(ITableView source, IAliasTable<TTable> target)
        : base(source, target)
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="source"></param>
    public AliasTableSelect(IAliasTable<TTable> source)
        : this(source, source)
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="source"></param>
    /// <param name="where"></param>
    public AliasTableSelect(IAliasTable<TTable> source, ISqlLogic where)
        : this(new TableFilter(source, where), source)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="query"></param>
    public AliasTableSelect(AliasTableSqlQuery<TTable> query)
        : this(query, query.Source)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="query"></param>
    public AliasTableSelect(AliasTableQuery<TTable> query)
        : this(query, query.Source)
    {
    }
}

