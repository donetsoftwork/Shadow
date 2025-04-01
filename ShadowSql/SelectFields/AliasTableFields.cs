using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace ShadowSql.SelectFields;

/// <summary>
/// 别名表字段筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
public class AliasTableFields<TTable>(TableAlias<TTable> source) :
    SelectFieldsBase<TableAlias<TTable>>(source), ISelectFields
    where TTable : ITable
{
    #region 配置
    private readonly TTable _table = source.Target;
    /// <summary>
    /// 原始表
    /// </summary>
    public TTable Table
        => _table;
    #endregion
    #region 功能
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    public AliasTableFields<TTable> Select(Func<IAliasTable, IField> select)
    {
        SelectCore(select(_source));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    public AliasTableFields<TTable> Select(Func<TTable, IColumn> select)
    {
        if (_source.GetPrefixColumn(select(_table)) is IPrefixColumn prefixColumn)
            SelectCore(prefixColumn);
        return this;
    }
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="field"></param>
    //public AliasTableFields<TTable> Select(IField field)
    //{
    //    SelectCore(field);
    //    return this;
    //}
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="columns"></param>
    ///// <returns></returns>
    //public AliasTableFields<TTable> Select(params IEnumerable<string> columns)
    //{
    //    SelectCore(columns);
    //    return this;
    //}
    ///// <summary>
    ///// 筛选别名
    ///// </summary>
    ///// <param name="alias"></param>
    ///// <param name="statement"></param>
    ///// <returns></returns>
    //public AliasTableFields<TTable> Alias(string alias, string statement)
    //{
    //    AliasCore(alias, statement);
    //    return this;
    //}
    #endregion
}
