using ShadowSql.Engines;
using ShadowSql.Generators;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 联表查询
/// </summary>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public class JoinTableQuery(IIdentifierGenerator aliasGenerator, SqlQuery filter)
    : MultiTableBase(aliasGenerator, filter), IMultiTableQuery
{
    /// <summary>
    /// 联表查询
    /// </summary>
    public JoinTableQuery()
        : this(new IdIncrementGenerator("t"), SqlQuery.CreateAndQuery())
    {        
    }
    #region 配置

    private readonly List<IJoinOn> _joinOns = [];

    /// <summary>
    /// 主表
    /// </summary>
    public IAliasTable Main
        => _tables[0];
    /// <summary>
    /// 联表信息
    /// </summary>
    public IEnumerable<IJoinOn> JoinOns
        => _joinOns;
    #endregion
    /// <summary>
    /// 添加联表信息
    /// </summary>
    /// <param name="joinOn"></param>
    internal void AddJoinOn(IJoinOn joinOn)
    {
        _joinOns.Add(joinOn);
    }

    #region ISqlEntity
    /// <summary>
    /// 拼写联表数据源sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void AcceptSource(ISqlEngine engine, StringBuilder sql)
    {
        Main.Write(engine, sql);
        foreach (var joinOn in _joinOns)
        {
            joinOn.Write(engine, sql);
        }
    }
    #endregion
}
