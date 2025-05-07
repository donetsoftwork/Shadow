using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using System;

namespace Dapper.Shadow.Cursors;

/// <summary>
/// 多联表范围筛选
/// </summary>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class DapperMultiTableCursor(IExecutor executor, IMultiView source, int limit, int offset)
    : MultiTableCursor(source, limit, offset)
{
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
    #region 功能
    #region TAliasTable
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    new public DapperMultiTableCursor Asc<TAliasTable>(string tableName, Func<TAliasTable, IOrderView> select)
        where TAliasTable : IAliasTable
    {
        var member = _source.From<TAliasTable>(tableName);
        AscCore(select(member));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    new public DapperMultiTableCursor Desc<TAliasTable>(string tableName, Func<TAliasTable, IOrderAsc> select)
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
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    new public DapperMultiTableCursor Asc<TTable>(string tableName, Func<TTable, IColumn> select)
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
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    new public DapperMultiTableCursor Desc<TTable>(string tableName, Func<TTable, IColumn> select)
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
