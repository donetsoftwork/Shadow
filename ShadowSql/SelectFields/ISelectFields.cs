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
    /// 被筛选列
    /// </summary>
    IEnumerable<IFieldView> Selected { get; }
    /// <summary>
    /// 输出被筛选列
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    bool WriteSelected(ISqlEngine engine, StringBuilder sql);
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="fields"></param>
    //void SelectCore(params IEnumerable<IFieldView> fields);
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="columns"></param>
    //void SelectCore(params IEnumerable<string> columns);
}
