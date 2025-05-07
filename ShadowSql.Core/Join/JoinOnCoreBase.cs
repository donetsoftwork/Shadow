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
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="filter"></param>
public abstract class JoinOnCoreBase<TJoinTable, TFilter>(TJoinTable root, IAliasTable left, IAliasTable right, TFilter filter)
    : JoinOnBase, IJoinOn, IMultiView, IDataFilter
    where TJoinTable : IJoinTable
    where TFilter : ISqlLogic
{

    #region 配置
    private readonly TJoinTable _root = root;
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
    /// <summary>
    /// 获取左边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IPrefixField? GetLeftField(string columName)
        => _left.GetPrefixField(columName);
    /// <summary>
    /// 获取右边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IPrefixField? GetRightField(string columName)
        => _source.GetPrefixField(columName);
    #endregion
    #region Field
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField LeftField(string fieldName)
        => _left.GetField(fieldName) ?? _left.NewField(fieldName);
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField RightField(string fieldName)
        => _source.GetField(fieldName) ?? _source.NewField(fieldName);
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
    IJoinTable IJoinOn.Root
        => _root;
    #region IDataFilter
    ITableView IDataFilter.Source
        => this;
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion
    #region FilterBase
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IField> GetFields()
       => _prefixFields;
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField? GetField(string fieldName)
        => GetPrefixField(fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override ICompareField GetCompareField(string fieldName)
    {
        return _source.GetPrefixField(fieldName)
            ?? _left.GetPrefixField(fieldName)
            ?? _source.NewField(fieldName);
    }
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