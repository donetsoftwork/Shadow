using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;

namespace ShadowSql.Join;

/// <summary>
/// 两表查询
/// </summary>
/// <typeparam name="LTable"></typeparam>
/// <typeparam name="RTable"></typeparam>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="query"></param>
public class PairMemberQuery<LTable, RTable>(TableAlias<LTable> left, TableAlias<RTable> right, DataQuery<IMultiTable> query)
    where LTable : ITable
    where RTable : ITable
{
    #region 配置
    private readonly TableAlias<LTable> _left = left;
    private readonly TableAlias<RTable> _right = right;

    /// <summary>
    /// 左表
    /// </summary>
    public TableAlias<LTable> Left
        => _left;
    /// <summary>
    /// 右表
    /// </summary>
    public TableAlias<RTable> Right
        => _right;
    /// <summary>
    /// 内部查询
    /// </summary>
    protected DataQuery<IMultiTable> _innerQuery = query;
    /// <summary>
    /// 内部查询
    /// </summary>
    public DataQuery<IMultiTable> InnerQuery
        => _innerQuery;
    #endregion
    #region 基础查询功能
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> Where(params IEnumerable<string> conditions)
    {
        _innerQuery.AddConditions(conditions);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> Where(AtomicLogic logic)
    {
        _innerQuery.AddLogic(logic);
        return this;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> Where(Func<SqlQuery, SqlQuery> query)
    {
        _innerQuery.ApplyQuery(query);
        return this;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> Where(Func<IMultiTable, SqlQuery, SqlQuery> query)
    {
        _innerQuery.ApplyQuery(query);
        return this;
    }
    /// <summary>
    /// 切换为And
    /// </summary>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> ToAnd()
    {
        _innerQuery.ToAndCore();
        return this;
    }
    /// <summary>
    /// 切换为Or
    /// </summary>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> ToOr()
    {
        _innerQuery.ToOrCore();
        return this;
    }
    #endregion
    #region 扩展查询功能
    #region 单表查询
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> WhereLeft(Func<IAliasTable, AtomicLogic> query)
    {
        _innerQuery.AddLogic(query(_left));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> WhereLeft(Func<LTable, IColumn> select, Func<IColumn, AtomicLogic> query)
    {
        //增加前缀
        var prefixColumn = _left.GetPrefixColumn(select(_left.Target));
        if (prefixColumn is not null)
            _innerQuery.AddLogic(query(prefixColumn));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> WhereRight(Func<IAliasTable, AtomicLogic> query)
    {
        _innerQuery.AddLogic(query(_right));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> WhereRight(Func<RTable, IColumn> select, Func<IColumn, AtomicLogic> query)
    {
        //增加前缀
        var prefixColumn = _right.GetPrefixColumn(select(_right.Target));
        if (prefixColumn is not null)
            _innerQuery.AddLogic(query(prefixColumn));
        return this;
    }
    #endregion
    #region 两表查询
    /// <summary>
    /// 按左表和右表的列比较逻辑查询
    /// </summary>
    /// <param name="left">左表的列</param>
    /// <param name="compare">比较类型</param>
    /// <param name="right">右表的列</param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> Where(Func<LTable, IColumn> left, CompareSymbol compare, Func<RTable, IColumn> right)
    {
        //增加前缀
        var leftColumn = _left.GetPrefixColumn(left(_left.Target));
        //增加前缀
        var rightColumn = _right.GetPrefixColumn(right(_right.Target));
        if (leftColumn is not null && rightColumn is not null)
            _innerQuery.AddLogic(leftColumn.Compare(compare, rightColumn));
        return this;
    }
    /// <summary>
    /// 按左表和右表的列相等逻辑查询
    /// </summary>
    /// <param name="left">左表的列</param>
    /// <param name="right">右表的列</param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> Where(Func<LTable, IColumn> left, Func<RTable, IColumn> right)
        => Where(left, CompareSymbol.Equal, right);
    #endregion
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public PairMemberQuery<LTable, RTable> Where(Func<IMultiTable, AtomicLogic> query)
    {
        _innerQuery.AddLogic(query(_innerQuery.Source));
        return this;
    }
    #endregion
}
