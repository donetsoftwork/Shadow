using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 成员表(支持查询当前表)
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="query"></param>
public class MemberQuery<TTable>(TableAlias<TTable> table, DataQuery<IMultiTable> query)
    : IDataQuery
    where TTable : ITable
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    private readonly TableAlias<TTable> _source = table;
    /// <summary>
    /// 数据源表
    /// </summary>
    public TableAlias<TTable> Source
        => _source;
    /// <summary>
    /// 内部查询
    /// </summary>
    private readonly DataQuery<IMultiTable> _innerQuery = query;
    /// <summary>
    /// 内部查询
    /// </summary>
    public DataQuery<IMultiTable> InnerQuery
        => _innerQuery;
    #endregion
    #region 查询扩展
    /// <summary>
    /// Where
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public MemberQuery<TTable> Where(Func<IAliasTable, SqlQuery, SqlQuery> query)
    {
        _innerQuery.ApplyQuery(q => query(_source, q));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public MemberQuery<TTable> Where(Func<IAliasTable, AtomicLogic> query)
    {
        _innerQuery.AddLogic(query(_source));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public MemberQuery<TTable> Where(Func<TTable, IColumn> select, Func<IColumn, AtomicLogic> query)
    {
        //增加前缀
        var prefixColumn = _source.GetPrefixColumn(select(_source.Target));
        if (prefixColumn is not null)
            _innerQuery.AddLogic(query(prefixColumn));
        return this;
    }
    /// <summary>
    /// 两表查询
    /// </summary>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="rightTableName"></param>
    /// <param name="leftColumn"></param>
    /// <param name="compare"></param>
    /// <param name="rightColumn"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public MemberQuery<TTable> Where<RTable>(string rightTableName, Func<TTable, IColumn> leftColumn, CompareSymbol compare, Func<RTable, IColumn> rightColumn)
        where RTable : ITable
    {
        var rightTable = _innerQuery.Source.Table<RTable>(rightTableName);
        //增加前缀
        var leftPrefixColumn = _source.GetPrefixColumn(leftColumn(_source.Target));
        //增加前缀
        var rightPrefixColumn = rightTable.GetPrefixColumn(rightColumn(rightTable.Target));
        if (leftPrefixColumn is not null && rightPrefixColumn is not null)
            _innerQuery.AddLogic(new CompareLogic(leftPrefixColumn, compare, rightPrefixColumn));

        return this;
    }
    /// <summary>
    /// 两表查询
    /// </summary>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="rightTableName"></param>
    /// <param name="leftColumn"></param>
    /// <param name="rightColumn"></param>
    /// <returns></returns>
    public MemberQuery<TTable> Where<RTable>(string rightTableName, Func<TTable, IColumn> leftColumn, Func<RTable, IColumn> rightColumn)
       where RTable : ITable
        => Where(rightTableName, leftColumn, CompareSymbol.Equal, rightColumn);
    #endregion
    #region IDataQuery
    void IDataQuery.AddConditions(IEnumerable<string> conditions)
        => _innerQuery.AddConditions(conditions);
    void IDataQuery.AddLogic(AtomicLogic condition)
        => _innerQuery.AddLogic(condition);
    void IDataQuery.ApplyQuery(Func<SqlQuery, SqlQuery> query)
        => _innerQuery.ApplyQuery(query);
    ICompareField IDataQuery.GetCompareField(string fieldName)
        => _innerQuery.GetCompareField(fieldName);
    SqlQuery IDataQuery.Filter
        => _innerQuery.Filter;
    #region ITableView
    ITableView IDataQuery.Source
        => _innerQuery.Source;
    IEnumerable<IColumn> ITableView.Columns
        => _innerQuery.Source.Columns;
    IColumn? ITableView.GetColumn(string columName)
        => _innerQuery.Source.GetColumn(columName);
    IField ITableView.Field(string fieldName)
        => _innerQuery.Source.Field(fieldName);
    #endregion
    #endregion
    #region ISqlEntity
    /// <summary>
    /// MemberQuery是辅助功能,不单独拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql) {}
    #endregion
}