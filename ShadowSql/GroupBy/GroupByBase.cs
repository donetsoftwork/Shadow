using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
public abstract class GroupByBase<TSource> : GroupByBase
    where TSource : ITableView
{
    /// <summary>
    /// 分组基类
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fields"></param>
    /// <param name="having"></param>
    public GroupByBase(TSource source, IFieldView[] fields, SqlQuery having)
        : base(fields, having)
    {
        _source = source;     
    }
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly TSource _source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public TSource Source
        => _source;
    #endregion
    /// <summary>
    /// 获取数据源
    /// </summary>
    /// <returns></returns>
    protected override ITableView GetSource()
        => _source;
    /// <summary>
    /// 输出数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void AcceptSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField Field(string fieldName)
        => _source.Field(fieldName);
}
/// <summary>
/// 分组基类
/// </summary>
public abstract class GroupByBase : IGroupByQuery
{
    /// <summary>
    /// 分组基类
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="having"></param>
    public GroupByBase(IFieldView[] fields, SqlQuery having)
    {
        _fields = fields;
        _columns = new(() => [.. fields.Select(field => field.ToColumn())]);
        _innerQuery = new DataQuery<IGroupByView>(this, having);
    }
    #region 配置
    private readonly IFieldView[] _fields;
    /// <summary>
    /// 分组字段
    /// </summary>
    public IFieldView[] Fields
        => _fields;
    private readonly Lazy<IColumn[]> _columns;
    /// <summary>
    /// 分组列
    /// </summary>
    public IColumn[] Columns
        => _columns.Value;
    /// <summary>
    /// 内部查询
    /// </summary>
    protected DataQuery<IGroupByView> _innerQuery;
    /// <summary>
    /// 内部查询
    /// </summary>
    public DataQuery<IGroupByView> InnerQuery
        => _innerQuery;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写分组条件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected void AcceptGroupBy(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        //必选的GROUP BY
        engine.GroupByPrefix(sql);
        var next = false;
        foreach (var field in _fields)
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
    /// 数据源拼写
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected abstract void AcceptSource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        AcceptSource(engine, sql);
        AcceptGroupBy(engine, sql);
        var point = sql.Length;
        engine.HavingPrefix(sql);
        if (!_innerQuery.Filter.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
    }
    #endregion
    /// <summary>
    /// 获取数据源
    /// </summary>
    /// <returns></returns>
    protected abstract ITableView GetSource();
    #region IDataView
    IEnumerable<IColumn> ITableView.Columns
        => Columns;
    ITableView IGroupByView.Source
        => GetSource();
    ITableView IDataQuery.Source
         => this;
    SqlQuery IDataQuery.Filter
        => _innerQuery.Filter;
    #region IDataQuery
    void IDataQuery.ApplyQuery(Func<SqlQuery, SqlQuery> query)
        => _innerQuery.ApplyQuery(query);
    void IDataQuery.AddConditions(IEnumerable<string> conditions)
        => _innerQuery.AddConditions(conditions);
    void IDataQuery.AddLogic(AtomicLogic condition)
        => _innerQuery.AddLogic(condition);
    IColumn? ITableView.GetColumn(string columName)
        => Columns.FirstOrDefault(column => column.IsMatch(columName));
    #endregion
    /// <summary>
    /// 获取字段信息
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField  Field(string fieldName);
    ICompareField IDataQuery.GetCompareField(string fieldName)
    {
        if (_fields.FirstOrDefault(field => field.IsMatch(fieldName)) is ICompareField compareField)
            return compareField;
        return Field(fieldName);
    }
    #endregion
}