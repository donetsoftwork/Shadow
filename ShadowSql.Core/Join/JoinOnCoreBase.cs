using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联基类
/// </summary>
/// <typeparam name="TFilter"></typeparam>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="filter"></param>
public abstract class JoinOnCoreBase<TFilter>(IJoinTable root, IAliasTable left, IAliasTable right, TFilter filter)
    : JoinOnBase(root), IJoinOn, IMultiView, IDataFilter
    where TFilter : ISqlLogic
{

    #region 配置
    /// <summary>
    /// 左表
    /// </summary>
    protected readonly IAliasTable _left = left;
    /// <summary>
    /// 当前数据源(右表)
    /// </summary>
    protected readonly IAliasTable _source = right;
    /// <summary>
    /// 左表
    /// </summary>
    public IAliasTable Left
        => _left;
    /// <summary>
    /// 当前数据源(右表)
    /// </summary>
    public IAliasTable Source
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