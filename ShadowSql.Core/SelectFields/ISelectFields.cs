using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SelectFields;

/// <summary>
/// 选择列
/// </summary>
public interface ISelectFields
{
    /// <summary>
    /// 被筛选字段
    /// </summary>
    IEnumerable<IFieldView> Selected { get; }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <returns></returns>
    IEnumerable<IColumn> ToColumns();
    /// <summary>
    /// 输出被筛选列
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    bool WriteSelected(ISqlEngine engine, StringBuilder sql);
}
