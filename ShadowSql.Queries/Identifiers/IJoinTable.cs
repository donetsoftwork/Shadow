using ShadowSql.Join;
using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 联表
/// </summary>
public interface IJoinTable : IMultiView
{
    /// <summary>
    /// 连接
    /// </summary>
    IEnumerable<IJoinOn> JoinOns { get; }
    /// <summary>
    /// 添加连接
    /// </summary>
    /// <param name="joinOn">联接</param>
    void AddJoinOn(IJoinOn joinOn);
}
