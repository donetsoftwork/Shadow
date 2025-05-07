using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组基类
/// </summary>
/// <typeparam name="TFilter"></typeparam>
/// <param name="fields"></param>
/// <param name="filter"></param>
public abstract class GroupByBase<TFilter>(IField[] fields, TFilter filter)
    : GroupByBase(fields), IDataFilter
    where TFilter : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 过滤条件
    /// </summary>
    internal TFilter _filter = filter;
    #endregion
    #region IDataFilter
    ITableView IDataFilter.Source
        => this;
    ISqlLogic IDataFilter.Filter
        => _filter;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 筛选条件可选
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override bool WriteFilter(ISqlEngine engine, StringBuilder sql)
        => _filter.TryWrite(engine, sql);
    #endregion
}
/// <summary>
/// 分组基类
/// </summary>
public abstract class GroupByBase : FilterBase, IGroupByView
{
    /// <summary>
    /// 分组基类
    /// </summary>
    /// <param name="fields"></param>
    public GroupByBase(IField[] fields)
    {
        _groupByFields = fields;
        _columns = new(() => [.. _groupByFields.Select(field => field.ToColumn())]);
    }
    #region 配置
    /// <summary>
    /// 分组数据源表
    /// </summary>
    public abstract ITableView Source { get; }
    private readonly IField[] _groupByFields;
    /// <summary>
    /// 分组字段
    /// </summary>
    public IField[] GroupByFields
        => _groupByFields;
    private readonly Lazy<IColumn[]> _columns;
    /// <summary>
    /// 分组列
    /// </summary>
    public IColumn[] Columns
        => _columns.Value;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        WriteGroupBySource(engine, sql);
        WriteGroupBy(engine, sql);
    }
    /// <summary>
    /// 拼写分组条件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected void WriteGroupBy(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        //必选的GROUP BY
        engine.GroupByPrefix(sql);
        var next = false;
        foreach (var field in _groupByFields)
        {
            var point2 = sql.Length;
            if (next)
                sql.Append(',');
            field.Write(engine, sql);
            next = true;

        }
        if (!next)
        {
            //回滚
            sql.Length = point;
            throw new InvalidOperationException("分组字段不能为空");
        }
    }
    /// <summary>
    /// 分组数据源拼写
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected abstract void WriteGroupBySource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 筛选前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void FilterPrefix(ISqlEngine engine, StringBuilder sql)
        => engine.HavingPrefix(sql);
    #endregion
    #region TableViewBase
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IField> GetFields()
        => _groupByFields;
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField? GetField(string fieldName)
        => _groupByFields.FirstOrDefault(f => f.IsMatch(fieldName));
    #endregion    
}