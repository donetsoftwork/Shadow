using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql.Tables;

/// <summary>
/// 逻辑查询
/// </summary>
public class TableQuery : DataFilterBase<Logic>, IDataQuery
{
    internal TableQuery(Logic filter, ITableView source)
        : base(source, filter)
    {
    }
    #region ITable
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    public TableQuery(ITable table, Logic filter)
        : this(filter, table)
    {
    }
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="table"></param>
    public TableQuery(ITable table)
        : this(new AndLogic(), table)
    {
    }
    #endregion
    #region TableName
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="filter"></param>
    public TableQuery(string tableName, Logic filter)
        : this(filter, EmptyTable.Use(tableName))
    {
    }
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName"></param>
    public TableQuery(string tableName)
        : this(new AndLogic(), EmptyTable.Use(tableName))
    {
    }
    #endregion
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
