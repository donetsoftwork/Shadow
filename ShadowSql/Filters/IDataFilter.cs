using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Filters;

/// <summary>
/// 数据过滤
/// </summary>
public interface IDataFilter : ITableView
{
    /// <summary>
    /// 数据源表
    /// </summary>
    ITableView Source { get; }
    /// <summary>
    /// 过滤逻辑
    /// </summary>
    ISqlLogic Filter { get; }
    /// <summary>
    /// 添加子逻辑
    /// </summary>
    /// <param name="condition"></param>
    void AddLogic(AtomicLogic condition);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    ICompareField GetCompareField(string fieldName);
}
/// <summary>
/// Where查询
/// </summary>
public interface IWhere : ISqlEntity;