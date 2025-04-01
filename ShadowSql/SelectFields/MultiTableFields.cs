using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

namespace ShadowSql.SelectFields;

/// <summary>
/// 多(联)表字段筛选
/// </summary>
/// <param name="source"></param>
public class MultiTableFields(IMultiView source) :
    SelectFieldsBase<IMultiView>(source), ISelectFields
{
    #region 功能
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    public MultiTableFields Select(Func<IMultiView, IFieldView> select)
    {
        SelectCore(select(_source));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select(string tableName, Func<IAliasTable, IFieldView> select)
    {
        var member = _source.From(tableName);
        SelectCore(select(member));
        return this;
    }
    /// <summary>
    /// 筛选多列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select(string tableName, Func<IAliasTable, IEnumerable<IFieldView>> select)
    {
        var member = _source.From(tableName);
        SelectCore(select(member));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        var member = _source.Table<TTable>(tableName);
        //增加前缀
        if (member.GetPrefixColumn(select(member.Target)) is IPrefixColumn prefixColumn)
            SelectCore(prefixColumn);
        return this;
    }
    /// <summary>
    /// 筛选多列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select<TTable>(string tableName, Func<TTable, IEnumerable<IColumn>> select)
        where TTable : ITable
    {
        var member = _source.Table<TTable>(tableName);
        var columns = select(member.Target);
        foreach (var column in columns)
        {
            //增加前缀
            if (member.GetPrefixColumn(column) is IPrefixColumn prefixColumn)
                SelectCore(prefixColumn);
        }
        return this;
    }
    #endregion
}
