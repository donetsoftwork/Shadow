using ShadowSql.Identifiers;
using ShadowSql.Queries;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class JoinOnSqlQuery(JoinTableSqlQuery root, IAliasTable left, IAliasTable right, SqlQuery onQuery)
    : JoinOnCoreBase<SqlQuery>(root, left, right, onQuery), IDataSqlQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnSqlQuery(JoinTableSqlQuery root, IAliasTable left, IAliasTable right)
        : this(root, left, right, SqlQuery.CreateAndQuery())
    { 
    }
    #region 配置
    private readonly JoinTableSqlQuery _root = root;
    /// <summary>
    /// 联表
    /// </summary>
    public new JoinTableSqlQuery Root
        => _root;
    #endregion
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
