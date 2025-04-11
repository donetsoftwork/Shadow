using ShadowSql.Filters;

namespace ShadowSql.Queries;

/// <summary>
/// 数据查询
/// </summary>
public interface IDataSqlQuery : IDataFilter
{
    /// <summary>
    /// sql查询
    /// </summary>
    SqlQuery Query { get; set; }
}
