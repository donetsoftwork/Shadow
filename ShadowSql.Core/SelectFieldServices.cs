using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;
using System;
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
    /// <param name="select"></param>
    /// <param name="fields"></param>
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
    /// <param name="select"></param>
    /// <param name="columns"></param>
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
    /// <param name="fields"></param>
    /// <param name="statement"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static TSelectFields Alias<TSelectFields>(this TSelectFields fields, string statement, string alias)
        where TSelectFields : SelectFieldsBase
    {
        fields.AliasCore(alias, statement);
        return fields;
    }
    #region 子查询
    /// <summary>
    /// 子查询作为字段
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="fields"></param>
    /// <param name="select"></param>
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
    /// <param name="fields"></param>
    /// <param name="select"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static TSelectFields Alias<TSelectFields>(this TSelectFields fields, ISingleSelect select, string alias)
        where TSelectFields : SelectFieldsBase
    {
        fields.SelectCore(select.ToField(alias));
        return fields;
    }
    #endregion
    #endregion
}
