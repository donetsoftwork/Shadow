using Hand.Structural;

namespace ShadowSql.Identifiers;

/// <summary>
/// 分组视图
/// </summary>
public interface IGroupByView
    : ITableView
    , IWrapper<ITableView>
{
    ///// <summary>
    ///// 分组数据源表
    ///// </summary>
    //ITableView Original { get; }
    /// <summary>
    /// 分组字段
    /// </summary>
    IField[] GroupByFields { get; }
}
