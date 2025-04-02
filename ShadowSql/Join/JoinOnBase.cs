﻿using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联基类
/// </summary>
/// <typeparam name="LTable"></typeparam>
/// <typeparam name="RTable"></typeparam>
/// <typeparam name="TFilter"></typeparam>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="filter"></param>
public abstract class JoinOnBase<LTable, RTable, TFilter>(IJoinTable root, TableAlias<LTable> left, TableAlias<RTable> right, TFilter filter)
    : JoinOnBase(root), IJoinOn, IMultiView, IDataFilter
    where LTable : ITable
    where RTable : ITable
    where TFilter : ISqlLogic
{

    #region 配置    
    private readonly TableAlias<LTable> _left = left;
    private readonly TableAlias<RTable> _source = right;
    /// <summary>
    /// 左表
    /// </summary>
    public TableAlias<LTable> Left
        => _left;
    /// <summary>
    /// 当前数据源(右表)
    /// </summary>
    public TableAlias<RTable> Source
        => _source;
    /// <summary>
    /// 表前缀包装列
    /// </summary>
    private readonly IPrefixColumn[] _prefixColumns = [.. left.PrefixColumns, .. right.PrefixColumns];
    /// <summary>
    /// 表前缀包装的列
    /// </summary>
    public IEnumerable<IPrefixColumn> PrefixColumns
        => _prefixColumns;
    /// <summary>
    /// 联表条件
    /// </summary>
    internal TFilter _filter = filter;
    #endregion
    #region Column
    /// <summary>
    /// 获取左边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IPrefixColumn? GetLeftColumn(string columName)
        => _left.GetPrefixColumn(columName);
    /// <summary>
    /// 获取右边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IPrefixColumn? GetRightColumn(string columName)
        => _source.GetPrefixColumn(columName);
    #endregion
    #region Field
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField LeftField(string fieldName)
        => _left.Field(fieldName);
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField RightField(string fieldName)
        => _source.Field(fieldName);
    #endregion
    #region IJoinOn
    IAliasTable IJoinOn.Left
        => _left;
    IAliasTable IJoinOn.JoinSource
        => _source;
    ISqlLogic IJoinOn.On
        => _filter;
    #endregion
    #region IMultiTable
    /// <summary>
    /// 获取联表成员
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override IAliasTable? GetMember(string name)
    {
        if (_source.IsMatch(name))
            return _source;
        if (_left.IsMatch(name))
            return _left;
        return null;
    }
    #endregion
    #region FilterBase
    /// <summary>
    /// 应用过滤
    /// </summary>
    /// <param name="filter"></param>
    internal void ApplyFilter(Func<TFilter, TFilter> filter)
        => _filter = filter(_filter);
    ///// <summary>
    ///// 过滤查询数据源
    ///// </summary>
    ///// <returns></returns>
    //protected override ITableView GetFilterSource()
    //    => this;
    #endregion
    #region IDataFilter
    ITableView IDataFilter.Source
        => this;
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion
    #region IDataView
    /// <summary>
    /// 获取所有列
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IColumn> GetColumns()
        => _prefixColumns;
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
    /// 拼写联接右表
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteRightSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    /// <summary>
    /// 拼写联表条件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _filter.TryWrite(engine, sql);
    #endregion
}
/// <summary>
/// 联表俩俩关联基类
/// </summary>
public abstract class JoinOnBase(IJoinTable root)
    : FilterBase, IMultiView
{
    #region 配置
    private readonly IJoinTable _root = root;
    /// <summary>
    /// 联表
    /// </summary>
    public IJoinTable Root
        => _root;
    private string _joinType = " INNER JOIN ";
    /// <summary>
    /// 联表类型
    /// </summary>
    public string JoinType
        => _joinType;
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
    #region JoinType
    private void AsType(string type)
       => _joinType = type;
    /// <summary>
    /// 内联
    /// </summary>
    /// <returns></returns>
    internal void AsInnerJoinCore()
        => AsType(" INNER JOIN ");
    /// <summary>
    /// 外联
    /// </summary>
    /// <returns></returns>
    internal void AsOuterJoinCore()
        => AsType(" OUTER JOIN ");
    /// <summary>
    /// 左联
    /// </summary>
    /// <returns></returns>
    internal void AsLeftJoinCore()
        => AsType(" LEFT JOIN ");
    /// <summary>
    /// 右联
    /// </summary>
    /// <returns></returns>
    internal void AsRightJoinCore()
        => AsType(" RIGHT JOIN ");
    #endregion
    #region Column
    /// <summary>
    /// 获取左边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IPrefixColumn? GetLeftColumn(string columName);
    /// <summary>
    /// 获取右边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IPrefixColumn? GetRightColumn(string columName);
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public IPrefixColumn? GetPrefixColumn(string columName)
    {
        return GetRightColumn(columName)
            ?? GetLeftColumn(columName);
    }
    #endregion
    #region Field
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField LeftField(string fieldName);
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField RightField(string fieldName);
    #endregion
    #region ICompareField
    /// <summary>
    /// 左边比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public ICompareView GetLeftCompareField(string fieldName)
    {
        if (GetLeftColumn(fieldName) is IColumn column)
            return column;
        return LeftField(fieldName);
    }
    /// <summary>
    /// 右边比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public ICompareView GetRightCompareField(string fieldName)
    {
        if (GetRightColumn(fieldName) is IColumn column)
            return column;
        return RightField(fieldName);
    }
    #endregion
    #region IMultiTable
    /// <summary>
    /// 获取联表成员
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract IAliasTable? GetMember(string tableName);
    #endregion
    //#region FilterBase
    ///// <summary>
    ///// 过滤查询数据源
    ///// </summary>
    ///// <returns></returns>
    //protected override ITableView GetFilterSource()
    //    => this;
    //#endregion
    #region ISqlEntity
    /// <summary>
    /// 筛选前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void FilterPrefix(ISqlEngine engine, StringBuilder sql)
        => engine.JoinOnPrefix(sql);
    /// <summary>
    /// 拼写联接右表
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void WriteRightSource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_joinType);
        WriteRightSource(engine, sql);
    }
    #endregion
}
