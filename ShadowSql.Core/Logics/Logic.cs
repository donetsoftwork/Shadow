using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Previews;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 逻辑基类
/// </summary>
/// <param name="separator"></param>
/// <param name="logics"></param>
public abstract class Logic(LogicSeparator separator, List<AtomicLogic> logics)
    : ISqlLogic, IPreview<AtomicLogic>
{
    /// <summary>
    /// 逻辑连接(and/or)
    /// </summary>
    internal readonly LogicSeparator _separator = separator;
    /// <summary>
    /// 子逻辑
    /// </summary>
    internal readonly List<AtomicLogic> _logics = logics;
    /// <summary>
    /// 子逻辑数量
    /// </summary>
    internal int LogicCount
        => _logics.Count;
    /// <summary>
    /// 第一个子逻辑
    /// </summary>
    internal AtomicLogic FirstLogic
        => _logics[0];
    /// <summary>
    /// 子逻辑预览
    /// </summary>
    /// <returns></returns>
    internal virtual IPreview<AtomicLogic> Preview()
        => this;
    /// <summary>
    /// 转化为And查询
    /// </summary>
    /// <returns></returns>
    public abstract Logic ToAnd();
    /// <summary>
    /// 转化为Or查询
    /// </summary>
    /// <returns></returns>
    public abstract Logic ToOr();
    #region 与逻辑
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public abstract Logic And(AtomicLogic atomic);
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public abstract Logic And(AndLogic and);
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public abstract Logic And(ComplexAndLogic and);
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public abstract Logic And(OrLogic or);
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public abstract Logic And(ComplexOrLogic or);
    #endregion
    #region 或逻辑
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    public abstract Logic Or(AtomicLogic atomic);
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public abstract Logic Or(OrLogic or);
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public abstract Logic Or(ComplexOrLogic or);
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public abstract Logic Or(AndLogic and);
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public abstract Logic Or(ComplexAndLogic and);
    #endregion
    /// <summary>
    /// 增加逻辑
    /// </summary>
    /// <param name="logic"></param>
    internal void AddLogic(AtomicLogic logic)
    {
        _logics.Add(logic);
    }
    /// <summary>
    /// 增加逻辑
    /// </summary>
    /// <param name="logics"></param>
    internal void AddLogics(IEnumerable<AtomicLogic> logics)
    {
        foreach (var item in logics)
            _logics.Add(item);
    }
    /// <summary>
    /// 增加反逻辑
    /// </summary>
    /// <param name="logics"></param>
    internal void NotLogics(IEnumerable<AtomicLogic> logics)
    {
        foreach (var item in logics)
            _logics.Add(item.Not());
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    internal virtual bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        var appended = false;
        foreach (var item in _logics)
        {
            var point = sql.Length;
            if (appended)
                _separator.Write(engine, sql);
            if (item.TryWrite(engine, sql))
            {
                appended = true;
            }
            else
            {
                //回滚
                sql.Length = point;
            }
        }

        return appended;
    }
    #region 运算符重载
    //Logic+类
    #region And
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(Logic logic, AtomicLogic other)
        => logic.And(other);
    #endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(Logic logic, AndLogic other)
        => logic.And(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(Logic logic, ComplexAndLogic other)
        => logic.And(other);
    #endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(Logic logic, OrLogic other)
        => logic.And(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(Logic logic, ComplexOrLogic other)
        => logic.And(other);
    #endregion
    #region Logic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(Logic logic, Logic other)
        => logic.And(other);
    #endregion
    #endregion
    #region Or
    #region AtomicLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(Logic logic, AtomicLogic other)
        => logic.Or(other);
    #endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(Logic logic, OrLogic other)
        => logic.Or(other);
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(Logic logic, ComplexOrLogic other)
        => logic.Or(other);
    #endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(Logic logic, AndLogic other)
        => logic.Or(other);
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(Logic logic, ComplexAndLogic other)
        => logic.Or(other);
    #endregion
    #region Logic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(Logic logic, Logic other)
        => logic.Or(other);
    #endregion
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Logic operator !(Logic logic)
        => logic.Not();
    #endregion
    #endregion
    #region IPreview<AtomicLogic>
    bool IPreview<AtomicLogic>.IsEmpty
        => _logics.Count == 0;
    AtomicLogic IPreview<AtomicLogic>.First
        => _logics[0];
    bool IPreview<AtomicLogic>.HasSecond
        => _logics.Count >= 2;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    bool ISqlFragment.TryWrite(ISqlEngine engine, StringBuilder sql)
        => TryWrite(engine, sql);
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    ISqlLogic ISqlLogic.Not()
        => throw new NotImplementedException("由子类实现");
}
