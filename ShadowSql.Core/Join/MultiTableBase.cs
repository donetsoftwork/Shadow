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
/// <param name="filter">过滤条件</param>
public abstract class MultiTableBase<TFilter>(TFilter filter)
    : MultiTableBase, IDataFilter, ITableView
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 过滤条件
    /// </summary>
    internal TFilter _filter = filter;
    #endregion
    #region IDataFilter
    /// <inheritdoc/>
    ITableView IDataFilter.Source
        => this;
    /// <inheritdoc/>
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _filter.TryWrite(engine, sql);
    #endregion
}
/// <summary>
/// 多(联)表基类
/// </summary>
public abstract class MultiTableBase
    : FilterBase, IMultiView, ITableView
{
    #region 配置
    /// <summary>
    /// 别名生成器
    /// </summary>
    private IIdentifierGenerator? _aliasGenerator;
    /// <summary>
    /// 别名生成器
    /// </summary>
    public IIdentifierGenerator AliasGenerator
    {
        set => _aliasGenerator = value;
    }
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
    /// 检查别名生成器
    /// </summary>
    /// <returns></returns>
    private IIdentifierGenerator CheckGenerator()
    {
        if (_aliasGenerator != null)
            return _aliasGenerator;
        return _aliasGenerator = new IdIncrementGenerator("t");
    }
    /// <summary>
    /// 构造新成员名
    /// </summary>
    /// <returns></returns>
    internal string CreateMemberName()
        => CheckGenerator()
            .NewName();
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    internal void AddMemberCore(IAliasTable aliasTable)
        => _tables.Add(aliasTable);
    #endregion
    #region IMultiTable
    /// <summary>
    /// 获取联表成员
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public IAliasTable? GetMember(string tableName)
        => _tables.FirstOrDefault(m => m.IsMatch(tableName));
    #endregion
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName">列名</param>
    /// <returns></returns>
    public IPrefixField? GetPrefixField(string columName)
    {
        foreach (var member in _tables)
        {
            if (member.GetPrefixField(columName) is IPrefixField prefixField)
                return prefixField;
        }
        return null;
    }
    #region IDataView
    /// <summary>
    /// 列
    /// </summary>
    public IEnumerable<IPrefixField> PrefixFields
        => _tables.SelectMany(m => m.PrefixFields);
    #endregion    
    #region FilterBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _tables.SelectMany(m => m.PrefixFields);
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => GetPrefixField(fieldName);
    /// <inheritdoc/>
    protected override ICompareField GetCompareField(string fieldName)
    {
        if(GetPrefixField(fieldName) is IPrefixField field)
            return field;
        return Column.Use(fieldName);
    }
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => Column.Use(fieldName);
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException"></exception>
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
