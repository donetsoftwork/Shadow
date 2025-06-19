using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Queries;

namespace ShadowSql.Tables;

/// <summary>
/// sql查询表
/// </summary>
public class TableSqlQuery : DataFilterBase<SqlQuery>, IDataSqlQuery, IWhere
{
    internal TableSqlQuery(SqlQuery query, ITableView source)
        : base(source, query)
    {
    }
    #region ITable
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    public TableSqlQuery(ITable table, SqlQuery query)
        : this(query, table)
    {
    }
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="table">表</param>
    public TableSqlQuery(ITable table)
        : this(SqlQuery.CreateAndQuery(), table)
    {
    }
    #endregion
    #region TableName
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="query">查询</param>
    public TableSqlQuery(string tableName, SqlQuery query)
        : this(query, EmptyTable.Use(tableName))
    {
    }
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName">表名</param>
    public TableSqlQuery(string tableName)
        : this(SqlQuery.CreateAndQuery(), EmptyTable.Use(tableName))
    {
    }
    #endregion
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}