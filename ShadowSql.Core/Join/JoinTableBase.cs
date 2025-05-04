using ShadowSql.Engines;
using ShadowSql.Generators;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 联表基类
/// </summary>
/// <typeparam name="TFilter"></typeparam>
/// <param name="filter"></param>
public abstract class JoinTableBase<TFilter>(TFilter filter)
    : MultiTableBase<TFilter>(filter), IJoinTable
    where TFilter : ISqlLogic
{
    #region 配置
    private readonly List<IJoinOn> _joinOns = [];
    /// <summary>
    /// 联表信息
    /// </summary>
    public IEnumerable<IJoinOn> JoinOns
        => _joinOns;
    /// <summary>
    /// 主表
    /// </summary>
    public IAliasTable Main
        => _tables[0];
    #endregion
    /// <summary>
    /// 添加联表信息
    /// </summary>
    /// <param name="joinOn"></param>
    public void AddJoinOn(IJoinOn joinOn)
        => _joinOns.Add(joinOn);
    #region ISqlEntity
    /// <summary>
    /// 拼写联表数据源sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        Main.Write(engine, sql);
        foreach (var joinOn in _joinOns)
            joinOn.Write(engine, sql);
    }
    #endregion
}
