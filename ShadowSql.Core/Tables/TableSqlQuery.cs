using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Simples;

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
    /// <param name="table"></param>
    /// <param name="query"></param>
    public TableSqlQuery(ITable table, SqlQuery query)
        : this(query, table)
    {
    }
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="table"></param>
    public TableSqlQuery(ITable table)
        : this(SqlQuery.CreateAndQuery(), table)
    {
    }
    #endregion
    #region IAliasTable
    /// <summary>
    /// 查询别名表
    /// </summary>
    /// <param name="table"></param>
    /// <param name="query"></param>
    public TableSqlQuery(IAliasTable table, SqlQuery query)
        : this(query, table)
    {
    }
    /// <summary>
    /// 查询别名表
    /// </summary>
    /// <param name="source"></param>
    public TableSqlQuery(IAliasTable source)
        : this(SqlQuery.CreateAndQuery(), source)
    {
    }
    #endregion
    #region TableName
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    public TableSqlQuery(string tableName, SqlQuery query)
        : this(query, SimpleTable.Use(tableName))
    {
    }
    /// <summary>
    /// 查询表
    /// </summary>
    /// <param name="tableName"></param>
    public TableSqlQuery(string tableName)
        : this(SqlQuery.CreateAndQuery(), SimpleTable.Use(tableName))
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