using ShadowSql.FieldInfos;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;

namespace ShadowSql;

/// <summary>
/// 筛选字段扩展方法
/// </summary>
public static partial class ShadowSqlQueryServices
{
    #region Alias
    /// <summary>
    /// 筛选别名
    /// </summary>
    /// <typeparam name="TSelectFields"></typeparam>
    /// <param name="fields">字段</param>
    /// <param name="statement"></param>
    /// <param name="alias">别名</param>
    /// <returns></returns>
    public static TSelectFields Alias<TSelectFields>(this TSelectFields fields, string statement, string alias)
        where TSelectFields : SelectFieldsBase
    {
        fields.SelectCore(new RawFieldAliasInfo(statement, alias));
        return fields;
    }
    #endregion
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
    #endregion
}
