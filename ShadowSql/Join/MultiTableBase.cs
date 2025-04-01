using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Generators;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 多(联)表基类
/// </summary>
/// <typeparam name="TFilter"></typeparam>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public abstract class MultiTableBase<TFilter>(IIdentifierGenerator aliasGenerator, TFilter filter)
    : MultiTableBase(aliasGenerator), IDataFilter, ITableView, IWhere
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 过滤条件
    /// </summary>
    protected TFilter _filter = filter;
    /// <summary>
    /// 过滤条件
    /// </summary>
    public TFilter Filter
        => _filter;
    #endregion
    #region FilterBase
    /// <summary>
    /// 应用过滤
    /// </summary>
    /// <param name="filter"></param>
    internal void ApplyFilter(Func<TFilter, TFilter> filter)
        => _filter = filter(_filter);
    #endregion   
    #region IDataFilter
    ITableView IDataFilter.Source
        => this;
    ISqlLogic IDataFilter.Filter
        => _filter;
    void IDataFilter.AddLogic(AtomicLogic condition)
        => AddLogic(condition);
    ICompareField IDataFilter.GetCompareField(string fieldName)
        => GetCompareField(fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写过滤条件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _filter.TryWrite(engine, sql);    
    #endregion
}
/// <summary>
/// 多(联)表基类
/// </summary>
public abstract class MultiTableBase(IIdentifierGenerator aliasGenerator)
    : FilterBase, IMultiTable, ITableView
{
    #region 配置
    /// <summary>
    /// 别名生成器
    /// </summary>
    protected readonly IIdentifierGenerator _aliasGenerator = aliasGenerator;
    /// <summary>
    /// 成员表
    /// </summary>
    protected readonly List<IAliasTable> _tables = [];
    /// <summary>
    /// 成员表
    /// </summary>
    public IEnumerable<IAliasTable> Tables
        => _tables;
    #endregion
    #region AddMember
    /// <summary>
    /// 构造新成员名
    /// </summary>
    /// <returns></returns>
    internal string CreateMemberName()
        => _aliasGenerator.NewName();
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <param name="aliasTable"></param>
    internal void AddMemberCore(IAliasTable aliasTable)
        => _tables.Add(aliasTable);
    #endregion
    #region IMultiTable
    /// <summary>
    /// 获取联表成员
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public IAliasTable? GetMember(string tableName)
        => _tables.FirstOrDefault(m => m.IsMatch(tableName));
    string IMultiTable.CreateMemberName()
        => CreateMemberName();
    void IMultiTable.AddMember(IAliasTable aliasTable)
        => AddMemberCore(aliasTable);
    #endregion
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public IPrefixColumn? GetPrefixColumn(string columName)
    {
        foreach (var member in _tables)
        {
            if (member.GetPrefixColumn(columName) is IPrefixColumn prefixColumn)
                return prefixColumn;
        }
        return null;
    }
    #region FilterBase
    /// <summary>
    /// 过滤查询数据源
    /// </summary>
    /// <returns></returns>
    internal override ITableView GetFilterSource()
        => this;
    #endregion
    #region IDataView
    /// <summary>
    /// 列
    /// </summary>
    public IEnumerable<IPrefixColumn> PrefixColumns
        => _tables.SelectMany(m => m.PrefixColumns);
    #endregion
    /// <summary>
    /// 获取比较列
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    internal override ICompareField GetCompareField(string fieldName)
    {
        if (GetPrefixColumn(fieldName) is ICompareField prefixColumn)
            return prefixColumn;
        return Field(fieldName);
    }
    #region ITableView
    /// <summary>
    /// 获取所有列
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IColumn> GetColumns()
        => PrefixColumns;
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columName)
        => GetPrefixColumn(columName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写多表数据源sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        if (_tables.Count == 0)
            throw new InvalidOperationException("没有数据表");
        var appended = false;
        foreach (var member in _tables)
        {
            if (appended)
                sql.Append(',');
            member.Write(engine, sql);
            appended = true;
        }
    }
    #endregion
}
