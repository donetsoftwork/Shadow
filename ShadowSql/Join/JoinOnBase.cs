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
/// <typeparam name="TJoinTable"></typeparam>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight"></typeparam>
/// <typeparam name="LTable"></typeparam>
/// <typeparam name="RTable"></typeparam>
/// <typeparam name="TFilter"></typeparam>
/// <param name="joinTable">联表</param>
/// <param name="left">左</param>
/// <param name="right">右</param>
/// <param name="filter">过滤条件</param>
public abstract class JoinOnBase<TJoinTable, TLeft, TRight, LTable, RTable, TFilter>(TJoinTable joinTable, TLeft left, TRight right, TFilter filter)
    : JoinOnBase, IJoinOn, IMultiView, IDataFilter
    where TJoinTable : IJoinTable
    where TLeft : IAliasTable<LTable>
    where TRight : IAliasTable<RTable>
    where LTable : ITable
    where RTable : ITable
    where TFilter : ISqlLogic
{

    #region 配置
    /// <summary>
    /// 联表
    /// </summary>
    protected readonly TJoinTable _root = joinTable;
    /// <summary>
    /// 联表
    /// </summary>
    public TJoinTable Root
        => _root;
    /// <summary>
    /// 左表
    /// </summary>
    protected readonly TLeft _left = left;
    /// <summary>
    /// 当前数据源(右表)
    /// </summary>
    protected readonly TRight _source = right;
    /// <summary>
    /// 左表
    /// </summary>
    public TLeft Left
        => _left;
    /// <summary>
    /// 当前数据源(右表)
    /// </summary>
    public TRight Source
        => _source;
    /// <summary>
    /// 表前缀包装字段
    /// </summary>
    private readonly IPrefixField[] _prefixFields = [.. left.PrefixFields, .. right.PrefixFields];
    /// <summary>
    /// 表前缀包装的字段
    /// </summary>
    public IEnumerable<IPrefixField> PrefixFields
        => _prefixFields;
    /// <summary>
    /// 联表条件
    /// </summary>
    internal TFilter _filter = filter;
    #endregion
    #region Column
    /// <inheritdoc/>
    public override IPrefixField? GetLeftField(string columName)
        => _left.GetPrefixField(columName);
    /// <inheritdoc/>
    public override IPrefixField? GetRightField(string columName)
        => _source.GetPrefixField(columName);
    #endregion
    #region Field
    /// <inheritdoc/>
    public override IField LeftField(string fieldName)
        => _left.Field(fieldName);
    /// <inheritdoc/>
    public override IField RightField(string fieldName)
        => _source.Field(fieldName);
    #endregion
    #region IJoinOn
    /// <inheritdoc/>
    IJoinTable IJoinOn.Root
        => _root;
    /// <inheritdoc/>
    IAliasTable IJoinOn.Left
        => _left;
    /// <inheritdoc/>
    IAliasTable IJoinOn.JoinSource
        => _source;
    /// <inheritdoc/>
    ISqlLogic IJoinOn.On
        => _filter;
    #endregion
    #region IMultiTable
    /// <inheritdoc/>
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
    /// <param name="filter">过滤条件</param>
    internal void ApplyFilter(Func<TFilter, TFilter> filter)
        => _filter = filter(_filter);
    #endregion
    #region IDataFilter
    /// <inheritdoc/>
    ITableView IDataFilter.Source
        => this;
    /// <inheritdoc/>
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion
    #region FilterBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
       => _prefixFields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => GetPrefixField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
    /// <inheritdoc/>
    protected override ICompareField GetCompareField(string fieldName)
    {
        return _source.GetPrefixField(fieldName)
            ?? _left.GetPrefixField(fieldName)
            ?? _source.NewField(fieldName);
    }
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteRightSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    /// <inheritdoc/>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _filter.TryWrite(engine, sql);
    #endregion
}
