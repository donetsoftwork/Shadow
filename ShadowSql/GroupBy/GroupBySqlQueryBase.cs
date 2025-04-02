using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组sql查询基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public abstract class GroupBySqlQueryBase<TSource>(TSource source, IFieldView[] fields, SqlQuery having)
    : GroupByBase<SqlQuery>(fields, having), IDataSqlQuery
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly TSource _source = source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public TSource Source
        => _source;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 输出数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteGroupBySource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
