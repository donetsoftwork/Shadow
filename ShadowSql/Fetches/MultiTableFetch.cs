using ShadowSql.Identifiers;
using System;

namespace ShadowSql.Fetches;

/// <summary>
/// 多联表范围筛选
/// </summary>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class MultiTableFetch(IMultiView source, int limit, int offset)
    : FetchBase<IMultiView>(source, offset, limit)
{
    #region 功能
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFetch Asc(Func<IMultiView, IOrderView> select)
    {
        AscCore(select(_source));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFetch Desc(Func<IMultiView, IOrderAsc> select)
    {
        DescCore(select(_source));
        return this;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFetch Asc(string tableName, Func<IAliasTable, IOrderView> select)
    {
        var member = _source.From(tableName);
        AscCore(select(member));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFetch Desc(string tableName, Func<IAliasTable, IOrderAsc> select)
    {
        var member = _source.From(tableName);
        DescCore(select(member));
        return this;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFetch Asc<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        var member = _source.Table<TTable>(tableName);
        //增加前缀
        var prefixColumn = member.GetPrefixColumn(select(member.Target));
        if (prefixColumn is not null)
            AscCore(prefixColumn);
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFetch Desc<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        var member = _source.Table<TTable>(tableName);
        //增加前缀
        var prefixColumn = member.GetPrefixColumn(select(member.Target));
        if (prefixColumn is not null)
            DescCore(prefixColumn);
        return this;
    }    
    #endregion
}
