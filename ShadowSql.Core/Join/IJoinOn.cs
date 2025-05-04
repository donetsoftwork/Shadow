using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联
/// </summary>
public interface IJoinOn : IMultiView, ISqlEntity
{
    /// <summary>
    /// 联表
    /// </summary>
    IJoinTable Root { get; }
    /// <summary>
    /// 联表类型
    /// </summary>
    string JoinType { get; }
    /// <summary>
    /// 左表
    /// </summary>
    IAliasTable Left { get; }
    /// <summary>
    /// 右表
    /// </summary>
    IAliasTable JoinSource { get; }
    /// <summary>
    /// 联表条件
    /// </summary>
    ISqlLogic On { get; }
    /// <summary>
    /// 获取左表列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    IPrefixColumn? GetLeftColumn(string columName);
    /// <summary>
    /// 获取右表列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    IPrefixColumn? GetRightColumn(string columName);
}
