using ShadowSql.Identifiers;
using System;

namespace ShadowSql.Cursors;

/// <summary>
/// 多联表范围筛选游标
/// </summary>
/// <param name="multiView">多(联)表</param>
/// <param name="limit">筛选数量</param>
/// <param name="offset">跳过数量</param>
public class MultiTableCursor(IMultiView multiView, int limit = 0, int offset = 0)
    : CursorBase<IMultiView>(multiView, limit, offset)
{
    #region 功能
    #region TAliasTable
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursor Asc<TAliasTable>(string tableName, Func<TAliasTable, IOrderView> select)
        where TAliasTable : IAliasTable
    {
        var member = _source.From<TAliasTable>(tableName);
        AscCore(select(member));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursor Desc<TAliasTable>(string tableName, Func<TAliasTable, IOrderAsc> select)
        where TAliasTable : IAliasTable
    {
        var member = _source.From<TAliasTable>(tableName);
        DescCore(select(member));
        return this;
    }
    #endregion
    #region TTable
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursor Asc<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        var member = _source.Alias<TTable>(tableName);
        //增加前缀
        var prefixField = member.GetPrefixField(select(member.Target));
        if (prefixField is not null)
            AscCore(prefixField);
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursor Desc<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        var member = _source.Alias<TTable>(tableName);
        //增加前缀
        var prefixField = member.GetPrefixField(select(member.Target));
        if (prefixField is not null)
            DescCore(prefixField);
        return this;
    }
    #endregion
    #endregion
}
