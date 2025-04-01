using ShadowSql.Assigns;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
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
    IEnumerable<IAssignOperation> AssignInfos { get; }
    /// <summary>
    /// 增加修改信息
    /// </summary>
    /// <param name="operation"></param>
    IUpdate Set(IAssignOperation operation);
    ///// <summary>
    ///// 过滤条件
    ///// </summary>
    //ISqlLogic Filter { get; }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    IColumn? GetColumn(string columName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    IField Field(string fieldName);
    /// <summary>
    /// 获取赋值字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    IAssignView GetAssignField(string fieldName);
}
