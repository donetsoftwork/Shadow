using ShadowSql.Assigns;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql.Update;

/// <summary>
/// 修改数据
/// </summary>
public interface IUpdate : IExecuteSql
{
    /// <summary>
    /// 表
    /// </summary>
    ITableView Table { get; }
    /// <summary>
    /// 修改信息
    /// </summary>
    IEnumerable<IAssignInfo> AssignInfos { get; }
}
