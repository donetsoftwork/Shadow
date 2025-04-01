using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 多表查询基类
/// </summary>
public abstract class MultiQueryBase : IMultiTableQuery, IMultiTable
{
    /// <summary>
    /// 多表查询
    /// </summary>
    /// <param name="where"></param>
    public MultiQueryBase(SqlQuery where)
    {
        _innerQuery = new DataQuery<IMultiTable>(this, where);
    }
    #region 配置
    /// <summary>
    /// 内部查询
    /// </summary>
    protected DataQuery<IMultiTable> _innerQuery;
    /// <summary>
    /// 内部查询
    /// </summary>
    public DataQuery<IMultiTable> InnerQuery
        => _innerQuery;
    #endregion

    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        AcceptSource(engine, sql);
        AcceptFilter(engine, sql);
    }
    /// <summary>
    /// 拼写数据源(表)sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected abstract void AcceptSource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 筛选条件可选
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual bool AcceptFilter(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        engine.WherePrefix(sql);
        if (!_innerQuery.Filter.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
        return true;
    }
    #endregion
    /// <summary>
    /// 获取前缀列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IPrefixColumn? GetPrefixColumn(string columName);
    /// <summary>
    /// 前缀列
    /// </summary>
    public abstract IEnumerable<IPrefixColumn> PrefixColumns { get; }
    #region ITableView
    /// <summary>
    /// 列
    /// </summary>
    IEnumerable<IColumn> ITableView.Columns
        => PrefixColumns;
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    IColumn? ITableView.GetColumn(string columName)
        => GetPrefixColumn(columName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IField Field(string fieldName)
        => FieldInfo.Use(fieldName);
    #endregion
    #region IMultiTable
    /// <summary>
    /// 表
    /// </summary>
    public abstract IEnumerable<IAliasTable> Tables { get; }
    /// <summary>
    /// 获取成员表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract IAliasTable? GetMember(string tableName);
    #endregion
    #region IDataQuery
    void IDataQuery.AddConditions(IEnumerable<string> conditions)
        => _innerQuery.AddConditions(conditions);
    void IDataQuery.AddLogic(AtomicLogic condition)
        => _innerQuery.AddLogic(condition);
    void IDataQuery.ApplyQuery(Func<SqlQuery, SqlQuery> query)
        => _innerQuery.ApplyQuery(query);
    /// <summary>
    /// 获取比较列
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract ICompareField GetCompareField(string fieldName);
    ITableView IDataQuery.Source
        => this;
    SqlQuery IDataQuery.Filter
        => _innerQuery.Filter;
    #endregion
}
