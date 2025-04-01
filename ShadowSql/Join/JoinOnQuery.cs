using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <remarks>
/// 联表俩俩关联查询
/// </remarks>
public class JoinOnQuery<LTable, RTable> : IJoinOn
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="onQuery"></param>
    public JoinOnQuery(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> right, SqlQuery onQuery)
    {
        _root = root;
        _left = left;
        _source = right;
        _tables = [left, right];
        _innerQuery = new DataQuery<IMultiTable>(this, onQuery);
    }
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="source"></param>
    public JoinOnQuery(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> source)
        :this(root, left, source, SqlQuery.CreateAndQuery())
    {
    }
    #region 配置    
    private readonly JoinTableQuery _root;
    private string _joinType = " INNER JOIN ";
    private readonly TableAlias<LTable> _left;
    private readonly TableAlias<RTable> _source;
    /// <summary>
    /// 成员表
    /// </summary>
    private readonly IAliasTable[] _tables;
    /// <summary>
    /// 联表类型
    /// </summary>
    public string JoinType
        => _joinType;
    /// <summary>
    /// 联表
    /// </summary>
    public JoinTableQuery Root
        => _root;
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
    /// 内部查询
    /// </summary>
    protected DataQuery<IMultiTable> _innerQuery;
    /// <summary>
    /// 内部查询
    /// </summary>
    public DataQuery<IMultiTable> InnerQuery
        => _innerQuery;
    #endregion
    #region JoinType
    private JoinOnQuery<LTable, RTable> AsType(string type)
    {
        _joinType = type;
        return this;
    }
    /// <summary>
    /// 内联
    /// </summary>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> AsInnerJoin()
        => AsType(" INNER JOIN ");
    /// <summary>
    /// 外联
    /// </summary>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> AsOuterJoin()
        => AsType(" OUTER JOIN ");
    /// <summary>
    /// 左联
    /// </summary>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> AsLeftJoin()
        => AsType(" LEFT JOIN ");
    /// <summary>
    /// 右联
    /// </summary>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> AsRightJoin()
        => AsType(" RIGHT JOIN ");
    #endregion
    #region 基础查询功能
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> On(params IEnumerable<string> conditions)
    {
        _innerQuery.AddConditions(conditions);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> On(AtomicLogic logic)
    {
        _innerQuery.AddLogic(logic);
        return this;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> On(Func<SqlQuery, SqlQuery> query)
    {
        _innerQuery.ApplyQuery(query);
        return this;
    }
    /// <summary>
    /// 切换为And
    /// </summary>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> ToAnd()
    {
        _innerQuery.ToAndCore();
        return this;
    }
    /// <summary>
    /// 切换为Or
    /// </summary>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> ToOr()
    {
        _innerQuery.ToOrCore();
        return this;
    }
    #endregion
    #region 单表查询
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> WhereLeft(Func<IAliasTable, AtomicLogic> query)
    {
        Root.InnerQuery.AddLogic(query(_left));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> WhereLeft(Func<LTable, IColumn> select, Func<IColumn, AtomicLogic> query)
    {
        //增加前缀
        var prefixColumn = _left.GetPrefixColumn(select(_left.Target));
        if (prefixColumn is not null)
            Root.InnerQuery.AddLogic(query(prefixColumn));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> WhereRight(Func<IAliasTable, AtomicLogic> query)
    {
        Root.InnerQuery.AddLogic(query(_source));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> WhereRight(Func<RTable, IColumn> select, Func<IColumn, AtomicLogic> query)
    {
        //增加前缀
        var prefixColumn = _source.GetPrefixColumn(select(_source.Target));
        if (prefixColumn is not null)
            Root.InnerQuery.AddLogic(query(prefixColumn));
        return this;
    }
    #endregion
    #region 扩展查询功能
    ///// <summary>
    ///// 按列联表
    ///// </summary>
    ///// <param name="left"></param>
    ///// <param name="right"></param>
    ///// <returns></returns>
    //public JoinOnQuery<LTable, RTable> On(Func<LTable, IColumn> left, Func<RTable, IColumn> right)
    //    => On(left, CompareSymbol.Equal, right);
    ///// <summary>
    ///// 按列联表
    ///// </summary>
    ///// <param name="left"></param>
    ///// <param name="compare"></param>
    ///// <param name="right"></param>
    ///// <returns></returns>
    //public JoinOnQuery<LTable, RTable> On(Func<LTable, IColumn> left, CompareSymbol compare, Func<RTable, IColumn> right)
    //{
    //    var leftColumn = _left.SelectPrefixColumn(left);
    //    var rightColumn = _source.SelectPrefixColumn(right);
    //    if (leftColumn != null && rightColumn != null)
    //        _innerQuery.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));

    //    return this;
    //}
    ///// <summary>
    ///// 按字段联表
    ///// </summary>
    ///// <param name="left"></param>
    ///// <param name="compare"></param>
    ///// <param name="right"></param>
    ///// <returns></returns>
    //public JoinOnQuery<LTable, RTable> On(string left, CompareSymbol compare, string right)
    //{
    //    if (_source.GetPrefixColumn(right) is not ICompareField rightField)
    //        rightField = _source.Field(right);
    //    if (_left.GetPrefixColumn(left) is IColumn leftColumn)
    //        _innerQuery.AddLogic(leftColumn.Compare(compare, rightField));
    //    else
    //        _innerQuery.AddLogic(_left.Field(left).Compare(compare, rightField));
    //    return this;
    //}
    ///// <summary>
    ///// 按字段联表
    ///// </summary>
    ///// <param name="left"></param>
    ///// <param name="right"></param>
    ///// <returns></returns>
    //public JoinOnQuery<LTable, RTable> On(string left, string right)
    //    => On(left, CompareSymbol.Equal, right);
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> On(Func<IAliasTable, IAliasTable, AtomicLogic> query)
    {
        _innerQuery.AddLogic(query(_left, _source));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> On(Func<IMultiTable, AtomicLogic> query)
    {
        _innerQuery.AddLogic(query(this));
        return this;
    }
    #endregion
    #region Column
    /// <summary>
    /// 获取左边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public IPrefixColumn? GetLeftColumn(string columName)
    {
        var leftColumn = _left.GetPrefixColumn(columName);
        if (leftColumn is not null)
            return leftColumn;
        foreach (var member in _root.Tables)
        {
            if (member == _left || member == _source)
                continue;
            leftColumn = member.GetPrefixColumn(columName);
            if (leftColumn is not null)
                return leftColumn;
        }
        return null;
    }
    /// <summary>
    /// 获取右边列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public IPrefixColumn? GetRightColumn(string columName)
        => _source.GetPrefixColumn(columName);
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public IPrefixColumn? GetPrefixColumn(string columName)
    {
        return _source.GetPrefixColumn(columName)
            ?? _left.GetPrefixColumn(columName);
    }
    #endregion
    #region Field
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IField LeftField(string fieldName)
        => _left.Field(fieldName);
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IField RightField(string fieldName)
        => _source.Field(fieldName);
    #endregion
    #region ICompareField
    /// <summary>
    /// 左边比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public ICompareView GetLeftCompareField(string fieldName)
    {
        if(GetLeftColumn(fieldName) is IColumn column)
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
    /// <summary>
    /// 获取联表成员
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IAliasTable? GetMember(string name)
    {
        if (_source.IsMatch(name))
            return _source;
        if (_left.IsMatch(name))
            return _left;
        return null;
    }

    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        //var point = sql.Length;
        sql.Append(_joinType);
        _source.Write(engine, sql);
        var point = sql.Length;
        engine.JoinOnPrefix(sql);
        if (!_innerQuery.Filter.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
        //if (_source.Write(engine, sql))
        //{
        //    var point2 = sql.Length;
        //    engine.JoinOnPrefix(sql);
        //    if (!_innerQuery.Filter.Write(engine, sql))
        //    {
        //        //回滚
        //        sql.Length = point2;
        //    }
        //    return true;
        //}
        //sql.Length = point;
        //return false;
    }
    #endregion

    #region IJoinOn
    IAliasTable IJoinOn.Left
        => _left;
    IAliasTable IJoinOn.Right
        => _source;
    ISqlLogic IJoinOn.On
        => _innerQuery.Filter;
    #endregion
    #region IDataView
    IColumn? ITableView.GetColumn(string columName)
        => GetPrefixColumn(columName);
    #region IMultiTableView
    IEnumerable<IAliasTable> IMultiTable.Tables
    {
        get { return [_left, _source]; }
    }
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IField Field(string fieldName)
        => FieldInfo.Use(fieldName);
    /// <summary>
    /// 表
    /// </summary>
    public IEnumerable<IAliasTable> Tables
        => _tables;
    IEnumerable<IColumn> ITableView.Columns
        => _left.PrefixColumns.Concat(_source.PrefixColumns);
    #endregion
    #endregion
}
