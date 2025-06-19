using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联基类
/// </summary>
/// <typeparam name="TJoinTable"></typeparam>
/// <typeparam name="TFilter"></typeparam>
/// <param name="joinTable">联表</param>
/// <param name="left">左</param>
/// <param name="right">右</param>
/// <param name="filter">过滤条件</param>
public abstract class JoinOnCoreBase<TJoinTable, TFilter>(TJoinTable joinTable, IAliasTable left, IAliasTable right, TFilter filter)
    : JoinOnBase, IJoinOn, IMultiView, IDataFilter
    where TJoinTable : IJoinTable
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
    private readonly IPrefixField[] _prefixFields = [.. left.PrefixFields, .. right.PrefixFields];
    /// <summary>
    /// 表前缀包装的列
    /// </summary>
    public IEnumerable<IPrefixField> PrefixFields
        => _prefixFields;
    /// <summary>
    /// 联表条件
    /// </summary>
    internal TFilter _filter = filter;
    #endregion
    #region Field
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
        => _left.GetField(fieldName) ?? _left.NewField(fieldName);
    /// <inheritdoc/>
    public override IField RightField(string fieldName)
        => _source.GetField(fieldName) ?? _source.NewField(fieldName);
    #endregion
    #region IJoinOn
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
    /// <inheritdoc/>
    IJoinTable IJoinOn.Root
        => _root;
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