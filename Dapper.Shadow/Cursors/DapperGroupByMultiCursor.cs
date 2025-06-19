using ShadowSql;
using ShadowSql.Aggregates;
using ShadowSql.Cursors;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace Dapper.Shadow.Cursors;

/// <summary>
/// 多(联)表分组后范围筛选
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="groupBy">分组查询</param>
/// <param name="limit">筛选数量</param>
/// <param name="offset">跳过数量</param>
public class DapperGroupByMultiCursor(IExecutor executor, GroupByMultiSqlQuery groupBy, int limit, int offset)
    : GroupByMultiCursor(groupBy, limit, offset)
{
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
    #region 聚合排序
    #region TAliasTable
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    new public DapperGroupByMultiCursor AggregateAsc<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> select)
        where TAliasTable : IAliasTable
    {
        var member = _multiTable.From<TAliasTable>(tableName);
        AscCore(select(member));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    new public DapperGroupByMultiCursor AggregateDesc<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> select)
        where TAliasTable : IAliasTable
    {
        DescCore(select(_multiTable.From<TAliasTable>(tableName)));
        return this;
    }
    #endregion
    #region TTable
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">选择表</param>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    new public DapperGroupByMultiCursor AggregateAsc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate)
        where TTable : ITable
    {
        var member = _multiTable.Alias<TTable>(tableName);
        //增加前缀
        if (member.GetPrefixField(select(member.Target)) is IPrefixField prefixField)
            AscCore(aggregate(prefixField));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">选择表</param>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    new public DapperGroupByMultiCursor AggregateDesc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate)
        where TTable : ITable
    {
        var member = _multiTable.Alias<TTable>(tableName);
        //增加前缀
        var prefixField = member.GetPrefixField(select(member.Target));
        if (prefixField is not null)
            DescCore(aggregate(prefixField));
        return this;
    }
    #endregion    
    #endregion
}
