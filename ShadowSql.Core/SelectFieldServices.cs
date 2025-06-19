using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;
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
    /// 筛选别名
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="fields">字段</param>
    /// <param name="statement"></param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static TSelectFields Alias<TSelectFields>(this TSelectFields fields, string statement, string aliasName)
        where TSelectFields : SelectFieldsBase
    {
        fields.AliasCore(aliasName, statement);
        return fields;
    }
    #region 子查询
    /// <summary>
    /// 子查询作为字段
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="fields">字段</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TSelectFields Select<TSelectFields>(this TSelectFields fields, ISingleSelect select)
        where TSelectFields : SelectFieldsBase
    {
        fields.SelectCore(select.ToField());
        return fields;
    }
    /// <summary>
    /// 子查询作为字段别名
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="fields">字段</param>
    /// <param name="select">筛选</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static TSelectFields Alias<TSelectFields>(this TSelectFields fields, ISingleSelect select, string aliasName)
        where TSelectFields : SelectFieldsBase
    {
        fields.SelectCore(select.ToField(aliasName));
        return fields;
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
    #endregion
}
