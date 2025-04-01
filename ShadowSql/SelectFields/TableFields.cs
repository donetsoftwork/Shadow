using ShadowSql.Identifiers;
using System;

namespace ShadowSql.SelectFields;

/// <summary>
/// 表字段筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
public class TableFields<TTable>(TTable table) :
    SelectFieldsBase<TTable>(table), ISelectFields
    where TTable : ITable
{
    #region 功能
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    public TableFields<TTable> Select(Func<TTable, IField> select)
    {
        SelectCore(select(_source));
        return this;
    }    
    #endregion
}
