using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using System.Text;

namespace ShadowSql.Expressions.GroupBy;

/// <summary>
/// 分组sql查询基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="view"></param>
/// <param name="fields">字段</param>
/// <param name="having">分组查询条件</param>
public abstract class GroupBySqlQueryBase<TSource>(TSource view, IField[] fields, SqlQuery having)
    : GroupByBase<SqlQuery>(fields, having), IDataSqlQuery
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    internal readonly TSource _source = view;
    /// <inheritdoc/>
    public override ITableView Source
        => _source;
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteGroupBySource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
    #region IDataQuery
    /// <inheritdoc/>
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
