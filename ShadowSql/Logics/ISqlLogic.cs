using ShadowSql.Fragments;

namespace ShadowSql.Logics;

/// <summary>
/// 逻辑条件
/// </summary>
public interface ISqlLogic : ISqlFragment
{
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    ISqlLogic Not();
}
