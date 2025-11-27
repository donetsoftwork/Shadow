using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 筛选字段扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{    
    #region SelectFields
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static TSelectFields Select<TSelectFields>(this TSelectFields select, params IEnumerable<IFieldView> fields)
        where TSelectFields : SelectFieldsBase
    {
        select.SelectCore(fields);
        return select;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TSelectFields Select<TSelectFields>(this TSelectFields select, params IEnumerable<string> columns)
        where TSelectFields : SelectFieldsBase
    {
        select.SelectCore(columns);
        return select;
    }
    /// <summary>
    /// 筛选自身所有字段
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static TSelectFields SelectSelfColumns<TSelectFields>(this TSelectFields fields)
        where TSelectFields : SelectFieldsBase
    {
        fields.SelfColumnsCore();
        return fields;
    }
    #endregion
}
