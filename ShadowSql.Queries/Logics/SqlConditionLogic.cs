using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// sql条件查询
/// </summary>
public class SqlConditionLogic : AtomicLogic, ISqlLogic
{
    /// <summary>
    /// sql条件查询
    /// </summary>
    /// <param name="separator"></param>
    public SqlConditionLogic(LogicSeparator separator)
        : this([], separator)
    {
    }
    /// <summary>
    /// sql条件查询
    /// </summary>
    /// <param name="items"></param>
    /// <param name="separator"></param>
    internal SqlConditionLogic(List<string> items, LogicSeparator separator)
        : this(new JoinFragment(items, separator.Separator), separator)
    {
    }
    /// <summary>
    /// sql条件查询
    /// </summary>
    /// <param name="fragment"></param>
    /// <param name="separator"></param>
    internal SqlConditionLogic(JoinFragment fragment, LogicSeparator separator)
    {
        _fragment = fragment;
        _separator = separator;
    }
    #region 配置
    private readonly JoinFragment _fragment;
    private readonly LogicSeparator _separator;
    /// <summary>
    /// 逻辑连接(and/or)
    /// </summary>
    public LogicSeparator Separator
        => _separator;
    /// <summary>
    /// sql片段组合
    /// </summary>
    internal JoinFragment Fragment 
        => _fragment;
    /// <summary>
    /// 子片段数量
    /// </summary>
    public int ItemsCount
        => _fragment.Count;
    #endregion
    #region AtomicLogicBase
    /// <inheritdoc/>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
        => _fragment.TryWrite(engine, sql);
    /// <inheritdoc/>
    public override AtomicLogic Not()
    {
        return _fragment.Count switch
        {
            1 => NotStatementLogic.CreateLogic(_fragment[0]),
            _ => new NotWrapLogic(this),
        };
    }
    #endregion
    /// <inheritdoc/>
    ISqlLogic ISqlLogic.Not()
    {
        return _fragment.Count switch
        {
            0 => EmptyLogic.Instance,
            1 => NotStatementLogic.CreateLogic(_fragment[0]),
            _ => new NotWrapLogic(this),
        };
    }
}
