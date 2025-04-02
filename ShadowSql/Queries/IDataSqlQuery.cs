using ShadowSql.Filters;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

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
    ///// <summary>
    ///// 数据源表
    ///// </summary>
    //ITableView Source { get; }
    ///// <summary>
    ///// Sql查询
    ///// </summary>
    //ISqlLogic Filter { get; }
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    ///// <returns></returns>
    //IDataQuery ToAnd();
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    ///// <returns></returns>
    //IDataQuery ToOr();
    ///// <summary>
    ///// 添加子逻辑
    ///// </summary>
    ///// <param name="condition"></param>
    //void AddLogic(AtomicLogic condition);
    ///// <summary>
    ///// And查询
    ///// </summary>
    ///// <param name="conditions"></param>
    ///// <returns></returns>
    //void AddConditions(IEnumerable<string> conditions);
    ///// <summary>
    ///// 应用查询
    ///// </summary>
    ///// <param name="query"></param>
    //void ApplyFilter(Func<SqlQuery, SqlQuery> query);
    ///// <summary>
    ///// 添加子逻辑
    ///// </summary>
    ///// <param name="logicInfo"></param>
    //void AddLogicInfo(ILogicInfo logicInfo);
    ///// <summary>
    ///// 获取列
    ///// </summary>
    ///// <param name="columName"></param>
    ///// <returns></returns>
    //IColumn? GetColumn(string columName);
    ///// <summary>
    ///// 获取字段
    ///// </summary>
    ///// <param name="fieldName"></param>
    ///// <returns></returns>
    //IField Field(string fieldName);
    ///// <summary>
    ///// 获取比较字段
    ///// </summary>
    ///// <param name="fieldName"></param>
    ///// <returns></returns>
    //ICompareField GetCompareField(string fieldName);
}
