using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Logics;
using ShadowSql.Previews;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Queries;

/// <summary>
/// Sql查询基类
/// </summary>
public abstract class SqlQuery(ComplexLogicBase complex, SqlConditionLogic conditions)
    : ISqlLogic
{
    #region 配置
    /// <summary>
    /// 复合逻辑
    /// </summary>
    internal readonly ComplexLogicBase _complex = complex;
    /// <summary>
    /// Sql查询条件
    /// </summary>
    protected readonly SqlConditionLogic _conditions = conditions;
    /// <summary>
    /// Sql查询条件
    /// </summary>
    public SqlConditionLogic Conditions
        => _conditions;
    /// <summary>
    /// 预览
    /// </summary>
    /// <returns></returns>
    internal IPreview<AtomicLogic> Preview()
        => new SqlQueryPreview(_conditions, _complex);
    #endregion
    #region Create
    ///// <summary>
    ///// 默认为CreateAndQuery
    ///// </summary>
    ///// <param name="conditions"></param>
    ///// <returns></returns>
    //public static SqlAndQuery Where(params IEnumerable<string> conditions)
    //    => CreateAndQuery(conditions);
    /// <summary>
    /// 构造查询
    /// </summary>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// var query = SqlQuery.CreateAndQuery("Id=@Id", "Status=@Status");
    /// </code>
    /// </example>
    public static SqlAndQuery CreateAndQuery()
        => new();
    /// <summary>
    /// 构造Or查询
    /// </summary>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// var query = SqlQuery.CreateOrQuery("Id=@Id", "Status=@Status");
    /// </code>
    /// </example>
    public static SqlOrQuery CreateOrQuery()
        => new();
    #endregion
    /// <summary>
    /// 增加sql条件
    /// </summary>
    /// <param name="conditions"></param>
    internal void AddConditions(params IEnumerable<string> conditions)
    {
        _conditions.Fragment.Add(conditions);
    }
    /// <summary>
    /// 增加逻辑
    /// </summary>
    /// <param name="atomic"></param>
    internal void AddLogic(AtomicLogic atomic)
    {
        _complex.AddLogic(atomic);
    }
    #region And
    /// <summary>
    /// And查询
    /// </summary>
    /// <returns></returns>
    public abstract SqlAndQuery ToAnd();
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public virtual SqlAndQuery And(params IEnumerable<string> conditions)
    {
        return ToAnd().And(conditions);
    }
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public virtual SqlAndQuery And(AtomicLogic atomic)
    {
        return ToAnd().And(atomic);
    }
    #endregion
    #region Or
    /// <summary>
    /// Or查询
    /// </summary>
    /// <returns></returns>
    public abstract SqlOrQuery ToOr();
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public virtual SqlOrQuery Or(params IEnumerable<string> conditions)
    {
        return ToOr().Or(conditions);
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public virtual SqlOrQuery Or(AtomicLogic atomic)
    {
        return ToOr().Or(atomic);
    }
    #endregion
    /// <summary>
    /// 复制查询
    /// </summary>
    /// <returns></returns>
    public abstract SqlQuery CopyQuery();
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    ISqlLogic ISqlLogic.Not()
        => throw new System.NotImplementedException("由子类实现");
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        bool appended = _complex.TryWrite(engine, sql);
        return _conditions.Fragment.TryWrite(engine, sql, appended);
    }    
}
