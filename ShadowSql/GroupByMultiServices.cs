//using ShadowSql.Aggregates;
//using ShadowSql.GroupBy;
//using ShadowSql.Identifiers;
//using ShadowSql.Logics;
//using System;

//namespace ShadowSql;

///// <summary>
///// 多(联)表分组扩展方法
///// </summary>
//public static partial class ShadowSqlServices
//{
//    #region GroupByMultiSqlQuery
//    /// <summary>
//    /// 按聚合逻辑查询
//    /// </summary>
//    /// <typeparam name="TGroupByMulti"></typeparam>
//    /// <typeparam name="TTable"></typeparam>
//    /// <param name="groupBy">分组查询</param>
//    /// <param name="tableName">表名</param>
//    /// <param name="aggregate">聚合</param>
//    /// <param name="query">查询</param>
//    /// <returns></returns>
//    public static TGroupByMulti HavingAggregate<TGroupByMulti, TTable>(TGroupByMulti groupBy, string tableName, Func<IAliasTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
//        where TGroupByMulti : GroupByMultiSqlQuery
//        where TTable : ITable
//    {
//        groupBy._filter.AddLogic(query(groupBy.Source.Aggregate(tableName, aggregate)));
//        return groupBy;
//    }
//    /// <summary>
//    /// 按聚合逻辑查询
//    /// </summary>
//    /// <typeparam name="TGroupByMulti"></typeparam>
//    /// <typeparam name="TTable"></typeparam>
//    /// <param name="groupBy">分组查询</param>
//    /// <param name="tableName">表名</param>
//    /// <param name="select">筛选</param>
//    /// <param name="aggregate">聚合</param>
//    /// <param name="query">查询</param>
//    /// <returns></returns>
//    public static TGroupByMulti HavingAggregate<TGroupByMulti, TTable>(TGroupByMulti groupBy, string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
//        where TGroupByMulti : GroupByMultiSqlQuery
//        where TTable : ITable
//    {
//        var member = groupBy.Source.Alias<TTable>(tableName);
//        groupBy._filter.AddLogic(query(aggregate(member.Prefix(select(member.Target)))));
//        return groupBy;
//    }
//    #endregion
//    #region GroupByMultiQuery
//    /// <summary>
//    /// 聚合逻辑与
//    /// </summary>
//    /// <typeparam name="TGroupByMulti"></typeparam>
//    /// <typeparam name="TTable"></typeparam>
//    /// <param name="groupBy">分组查询</param>
//    /// <param name="tableName">表名</param>
//    /// <param name="aggregate">聚合</param>
//    /// <param name="query">查询</param>
//    /// <returns></returns>
//    public static TGroupByMulti And<TGroupByMulti, TTable>(TGroupByMulti groupBy, string tableName, Func<IAliasTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
//        where TGroupByMulti : GroupByMultiQuery
//        where TTable : ITable
//    {
//        groupBy._filter = groupBy._filter.And(query(groupBy.Source.Aggregate(tableName, aggregate)));
//        return groupBy;
//    }
//    /// <summary>
//    /// 聚合逻辑或
//    /// </summary>
//    /// <typeparam name="TGroupByMulti"></typeparam>
//    /// <typeparam name="TTable"></typeparam>
//    /// <param name="groupBy">分组查询</param>
//    /// <param name="tableName">表名</param>
//    /// <param name="aggregate">聚合</param>
//    /// <param name="query">查询</param>
//    /// <returns></returns>
//    public static TGroupByMulti Or<TGroupByMulti, TTable>(TGroupByMulti groupBy, string tableName, Func<IAliasTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
//        where TGroupByMulti : GroupByMultiQuery
//        where TTable : ITable
//    {
//        groupBy._filter = groupBy._filter.Or(query(groupBy.Source.Aggregate(tableName, aggregate)));
//        return groupBy;
//    }
//    /// <summary>
//    /// 聚合逻辑与
//    /// </summary>
//    /// <typeparam name="TGroupByMulti"></typeparam>
//    /// <typeparam name="TTable"></typeparam>
//    /// <param name="groupBy">分组查询</param>
//    /// <param name="tableName">表名</param>
//    /// <param name="select">筛选</param>
//    /// <param name="aggregate">聚合</param>
//    /// <param name="query">查询</param>
//    /// <returns></returns>
//    public static TGroupByMulti And<TGroupByMulti, TTable>(TGroupByMulti groupBy, string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
//        where TGroupByMulti : GroupByMultiQuery
//        where TTable : ITable
//    {
//        var member = groupBy.Source.Alias<TTable>(tableName);
//        groupBy._filter = groupBy._filter.And(query(aggregate(member.Prefix(select(member.Target)))));
//        return groupBy;
//    }
//    /// <summary>
//    /// 聚合逻辑或
//    /// </summary>
//    /// <typeparam name="TGroupByMulti"></typeparam>
//    /// <typeparam name="TTable"></typeparam>
//    /// <param name="groupBy">分组查询</param>
//    /// <param name="tableName">表名</param>
//    /// <param name="select">筛选</param>
//    /// <param name="aggregate">聚合</param>
//    /// <param name="query">查询</param>
//    /// <returns></returns>
//    public static TGroupByMulti Or<TGroupByMulti, TTable>(TGroupByMulti groupBy, string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
//        where TGroupByMulti : GroupByMultiQuery
//        where TTable : ITable
//    {
//        var member = groupBy.Source.Alias<TTable>(tableName);
//        groupBy._filter = groupBy._filter.Or(query(aggregate(member.Prefix(select(member.Target)))));
//        return groupBy;
//    }
//    #endregion
//}
