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
    /// <param name="view"></param>
    /// <param name="aliasTable">别名表</param>
    internal AliasTableSelect(ITableView view, IAliasTable<TTable> aliasTable)
        : base(view, aliasTable)
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    public AliasTableSelect(IAliasTable<TTable> aliasTable)
        : this(aliasTable, aliasTable)
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    public AliasTableSelect(IAliasTable<TTable> aliasTable, ISqlLogic where)
        : this(new TableFilter(aliasTable, where), aliasTable)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="query">查询</param>
    public AliasTableSelect(AliasTableSqlQuery<TTable> query)
        : this(query, query.Source)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="query">查询</param>
    public AliasTableSelect(AliasTableQuery<TTable> query)
        : this(query, query.Source)
    {
    }
}

